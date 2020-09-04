using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using NetOpnApi.Errors;
using NetOpnApi.Models;

namespace NetOpnApi
{
    /// <summary>
    /// Helpful extension methods for commands.
    /// </summary>
    public static class CommandExtensions
    {
        private static HttpClient ValidatedClient { get; }

        private static HttpClient NonValidatedClient { get; }

        static CommandExtensions()
        {
            var validatedHandler = new HttpClientHandler();

            var nonValidatedHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, certificate, chain, error) => true
            };

            ValidatedClient    = new HttpClient(validatedHandler);
            NonValidatedClient = new HttpClient(nonValidatedHandler);
        }

        #region Generic Interface Helpers

        private static bool ImplementsGeneric(this Type type, Type genericInterfaceType, params Type[] genericArgs)
        {
            if (type is null) return false;
            if (!genericInterfaceType.IsInterface) throw new ArgumentException("must be an interface type", nameof(genericInterfaceType));
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == genericInterfaceType)
            {
                return genericArgs is null || genericArgs.Length < 1 || type.GetGenericArguments().SequenceEqual(genericArgs);
            }

            return type.GetInterfaces().Any(x => x.ImplementsGeneric(genericInterfaceType, genericArgs));
        }

        private static Type GetGenericArg(this Type type, Type genericInterfaceType, int argIndex = 0)
        {
            if (type is null) return null;
            if (!genericInterfaceType.IsInterface) throw new ArgumentException("must be an interface type", nameof(genericInterfaceType));

            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == genericInterfaceType)
            {
                var args = type.GetGenericArguments();
                if (argIndex < 0 ||
                    argIndex >= args.Length) throw new IndexOutOfRangeException();
                return args[argIndex];
            }

            return type.GetInterfaces()
                       .FirstOrDefault(x => x.ImplementsGeneric(genericInterfaceType))
                       ?.GetGenericArg(genericInterfaceType, argIndex);
        }

        #endregion

        #region Generic Response Helpers

        /// <summary>
        /// Determine if the command supports a response value.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static bool SupportsResponse(this ICommand command)
            => command?.GetType().ImplementsGeneric(typeof(ICommandWithResponse<>)) ?? false;

        /// <summary>
        /// Get the data type of the response value.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static Type GetResponseType(this ICommand command)
            => command?.GetType().GetGenericArg(typeof(ICommandWithResponse<>));

        /// <summary>
        /// Get the response value.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static object GetResponse(this ICommand command)
        {
            if (!command.SupportsResponse()) throw new ArgumentException("Command does not support a response.");

            var prop = command.GetType().GetProperty("Response", BindingFlags.Instance | BindingFlags.Public);
            if (prop is null) throw new ArgumentException("Command does not implement \"Response\" property publicly.");

            return prop.GetValue(command);
        }

        #endregion

        #region Create POST Content

        private static StringContent CreatePostContent(this ICommandWithParameterSet self)
        {
            var obj  = self?.GetRequestPayload();
            var type = self?.GetRequestPayloadDataType();

            if (obj == null) return null;

            var body = JsonSerializer.Serialize(obj, type);
            return new StringContent(body) {Headers = {ContentType = new MediaTypeHeaderValue("application/json")}};
        }

        private static StringContent CreatePostContent(this ICommand self)
        {
            if (self is ICommandWithParameterSet set)
            {
                var obj = set.GetRequestPayload();
                if (obj == null) return null;
                var type = set.GetRequestPayloadDataType();

                var body = JsonSerializer.Serialize(obj, type);
                return new StringContent(body) {Headers = {ContentType = new MediaTypeHeaderValue("application/json")}};
            }

            return null;
        }

        #endregion

        #region Get Response Root Element

        private static readonly JsonElement NullJsonObject  = JsonDocument.Parse("null").RootElement;
        private static readonly JsonElement EmptyJsonObject = JsonDocument.Parse("{}").RootElement;
        private static readonly JsonElement EmptyJsonArray  = JsonDocument.Parse("[]").RootElement;

        private static JsonElement GetResponseRootElement(this ICommand self, JsonDocument jsonDocument)
        {
            var root = jsonDocument.RootElement;

            // null returned from API, return null.
            if (root.ValueKind == JsonValueKind.Null) return NullJsonObject;

            // empty array returned from API where root element is named, return null.
            if (root.ValueKind == JsonValueKind.Array &&
                root.GetArrayLength() == 0 &&
                !string.IsNullOrEmpty(self.ResponseRootElementName))
            {
                if (string.IsNullOrEmpty(self.ResponseRootElementName))
                {
                    return self.ResponseRootElementValueKind == JsonValueKind.Object ? EmptyJsonObject
                           : self.ResponseRootElementValueKind == JsonValueKind.Array ? EmptyJsonArray
                           : NullJsonObject;
                }

                return NullJsonObject;
            }

            if (!string.IsNullOrEmpty(self.ResponseRootElementName))
            {
                if (root.ValueKind == JsonValueKind.Object)
                {
                    if (!root.TryGetProperty(self.ResponseRootElementName, out root))
                    {
                        // json response was object, but has no matching property, return null.
                        return NullJsonObject;
                    }
                }
                else
                {
                    throw new NetOpnApiInvalidResponseException(
                        ErrorCode.InvalidResponseRootObject,
                        "The json response does not contain an object."
                    );
                }
            }

            // no requirement on the root element kind.
            if (self.ResponseRootElementValueKind == JsonValueKind.Null)
            {
                return root;
            }
            
            if (root.ValueKind != self.ResponseRootElementValueKind)
            {
                // the root element contains an empty array where an object is expected, return an empty object.
                if (self.ResponseRootElementValueKind == JsonValueKind.Object &&
                    root.ValueKind == JsonValueKind.Array &&
                    root.GetArrayLength() == 0)
                {
                    return EmptyJsonObject;
                }

                if (string.IsNullOrEmpty(self.ResponseRootElementName))
                {
                    throw new NetOpnApiInvalidResponseException(
                        ErrorCode.InvalidResponseRootObject,
                        $"The root element of the json response is not an {self.ResponseRootElementValueKind}."
                    );
                }

                throw new NetOpnApiInvalidResponseException(
                    ErrorCode.InvalidResponseRootObject,
                    $"The root element property named {self.ResponseRootElementName} is not an {self.ResponseRootElementValueKind}."
                );
            }

            // root element found.
            return root;
        }

        #endregion

        #region Create Request URL

        private static void AppendCommandParametersToUrl(this ICommandWithParameterSet self, StringBuilder builder)
        {
            if (self is null) return;

            var parameters = self.GetUrlParameters();
            if (parameters != null &&
                parameters.Count > 0)
            {
                foreach (var parameter in parameters)
                {
                    builder.Append('/').Append(HttpUtility.UrlEncode(parameter));
                }
            }

            var query = self.GetQueryParameters();
            if (query != null &&
                query.Count > 0)
            {
                var first = true;
                foreach (var param in query)
                {
                    builder.Append(first ? '?' : '&');
                    first = false;
                    builder.Append(HttpUtility.UrlEncode(param.Key)).Append('=').Append(HttpUtility.UrlEncode(param.Value));
                }
            }
        }

        private static void AppendCommandParametersToUrl(this ICommand self, StringBuilder builder)
        {
            if (self is null) return;
            if (!(self is ICommandWithParameterSet set)) return;

            var parameters = set.GetUrlParameters();
            if (parameters != null &&
                parameters.Count > 0)
            {
                foreach (var parameter in parameters)
                {
                    builder.Append('/').Append(HttpUtility.UrlEncode(parameter));
                }
            }

            var query = set.GetQueryParameters();
            if (query != null &&
                query.Count > 0)
            {
                var first = true;
                foreach (var param in query)
                {
                    builder.Append(first ? '?' : '&');
                    first = false;
                    builder.Append(HttpUtility.UrlEncode(param.Key)).Append('=').Append(HttpUtility.UrlEncode(param.Value));
                }
            }
        }

        private static StringBuilder CreateRequestUrlCommon(this ICommand self)
        {
            var cfg = self.Config ?? throw new ArgumentNullException(nameof(self.Config));
            if (string.IsNullOrWhiteSpace(cfg.Host)) throw new ArgumentException("Config is invalid: host is blank.");
            var sb = new StringBuilder("https://");
            sb.Append(HttpUtility.UrlEncode(cfg.Host));
            if (cfg.Port != 0 &&
                cfg.Port != 443)
            {
                sb.Append(':').Append(cfg.Port);
            }

            sb.Append('/');

            var apiPath = cfg.ApiPath?.Trim('/') ?? "";
            if (apiPath != "")
            {
                sb.Append(apiPath);
            }

            sb.Append('/').Append(HttpUtility.UrlEncode(self.Module));
            sb.Append('/').Append(HttpUtility.UrlEncode(self.Controller));
            sb.Append('/').Append(HttpUtility.UrlEncode(self.Command));

            return sb;
        }

        private static Uri CreateRequestUrl(this ICommandWithParameterSet self)
        {
            var sb = self.CreateRequestUrlCommon();

            self.AppendCommandParametersToUrl(sb);

            return new Uri(sb.ToString());
        }

        private static Uri CreateRequestUrl(this ICommand self)
        {
            var sb = self.CreateRequestUrlCommon();

            self.AppendCommandParametersToUrl(sb);

            return new Uri(sb.ToString());
        }

        #endregion

        #region Create Request

        private static HttpRequestMessage CommonCreateRequest(this ICommand self, Uri uri, IDeviceConfig cfg, HttpMethod method, HttpContent content)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = uri,
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(
                            Encoding.ASCII.GetBytes(
                                $"{cfg.Key}:{cfg.Secret}"
                            )
                        )
                    )
                },
                Method  = method,
                Content = content
            };

            var contentBytes = content is null ? "" : $" ({content.Headers.ContentLength} bytes)";

            self.Logger?.LogInformation($"{request.Method.ToString().ToUpper()}: {request.RequestUri}{contentBytes}");

            return request;
        }

        private static HttpRequestMessage CreateRequest(this ICommandWithParameterSet self, IDeviceConfig cfg)
            => self.CommonCreateRequest(
                self.CreateRequestUrl(),
                cfg,
                self.UsePost ? HttpMethod.Post : HttpMethod.Get,
                self.UsePost ? self.CreatePostContent() : null
            );

        private static HttpRequestMessage CreateRequest(this ICommand self, IDeviceConfig cfg)
            => self.CommonCreateRequest(
                self.CreateRequestUrl(),
                cfg,
                self.UsePost ? HttpMethod.Post : HttpMethod.Get,
                self.UsePost ? self.CreatePostContent() : null
            );

        #endregion

        #region Set Response

        private static void SetEmptyResponse<T>(this ICommandWithResponse<T> self)
        {
            self.Response = default(T);
        }

        private static void SetEmptyResponse(this ICommand self)
        {
            if (!self.SupportsResponse()) return;

            var prop = self.GetType().GetProperty("Response", BindingFlags.Instance | BindingFlags.Public);
            if (prop is null) throw new ArgumentException("Command does not implement \"Response\" property publicly.");
            if (!prop.CanWrite) throw new ArgumentException("Command does not implement writable \"Response\" property publicly.");

            var t    = self.GetResponseType();
            var ctor = t.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null);
            if (t.IsValueType &&
                ctor != null)
            {
                prop.SetValue(self, ctor.Invoke(null));
            }
            else
            {
                prop.SetValue(self, null);
            }
        }

        private static void SetResponse<T>(this ICommandWithResponse<T> self, JsonElement value)
        {
            if (value.ValueKind == JsonValueKind.Null)
            {
                self.SetEmptyResponse();
                return;
            }

            var t = typeof(T);

            try
            {
                self.Logger?.LogDebug($"Deserializing {t} from JSON element.");
                self.Response = JsonSerializer.Deserialize<T>(value.ToString(), new JsonSerializerOptions());
            }
            catch (JsonException)
            {
                throw new ArgumentException($"Failed to decode {t} from JSON.");
            }
        }

        private static void SetResponse(this ICommand self, JsonElement value)
        {
            if (!self.SupportsResponse()) return;

            if (value.ValueKind == JsonValueKind.Null)
            {
                self.SetEmptyResponse();
                return;
            }

            var prop = self.GetType().GetProperty("Response", BindingFlags.Instance | BindingFlags.Public);
            if (prop is null) throw new ArgumentException("Command does not implement \"Response\" property publicly.");
            if (!prop.CanWrite) throw new ArgumentException("Command does not implement writable \"Response\" property publicly.");
            var t = self.GetResponseType();

            try
            {
                self.Logger?.LogDebug($"Deserializing {t} from JSON element.");
                prop.SetValue(self, JsonSerializer.Deserialize(value.ToString(), t));
            }
            catch (JsonException)
            {
                throw new ArgumentException($"Failed to decode {t} from JSON.");
            }
        }

        #endregion
        
        #region ExecuteCommand

        private static void ExecuteCommand(this ICommand self, Func<IDeviceConfig, HttpRequestMessage> createRequest, Action setEmpty, Action<JsonElement> setValue, int timeout)
        {
            if (self is null) throw new ArgumentNullException(nameof(self));
            var cfg    = self.Config ?? throw new ArgumentNullException(nameof(self.Config));
            var client = cfg.ValidateCertificate ? ValidatedClient : NonValidatedClient;
            try
            {
                using (var request = createRequest(cfg))
                {
                    var responseTask = client.SendAsync(request);
                    responseTask.Wait(TimeSpan.FromSeconds(timeout));
                    if (!responseTask.IsCompleted) throw new NetOpnApiTimeoutException();

                    using (var response = responseTask.Result)
                    {
                        self.Logger?.LogDebug($"Response Code: {(int) response.StatusCode} ({response.StatusCode})");

                        if (NetOpnApiNotImplementedException.HandledCodes.Contains(response.StatusCode))
                        {
                            throw new NetOpnApiNotImplementedException(response.StatusCode, self);
                        }

                        if (NetOpnApiNotAuthorizedException.HandledCodes.Contains(response.StatusCode))
                        {
                            throw new NetOpnApiNotAuthorizedException(response.StatusCode);
                        }

                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.NoContent:
                            case HttpStatusCode.ResetContent:
                                setEmpty();
                                break;

                            case HttpStatusCode.OK:
                            case HttpStatusCode.Accepted:
                            case HttpStatusCode.Created:
                            case HttpStatusCode.PartialContent:
                                var json = response.Content.ReadAsStringAsync().Result;

                                if (string.IsNullOrWhiteSpace(json))
                                {
                                    self.Logger?.LogDebug("No content returned.");
                                    setEmpty();
                                }
                                else
                                {
                                    if (!string.Equals("application/json", response.Content.Headers.ContentType.MediaType, StringComparison.OrdinalIgnoreCase))
                                    {
                                        json = JsonSerializer.Serialize(
                                            new Dictionary<string, string>()
                                            {
                                                {"content", json},
                                                {"contentType", response.Content.Headers.ContentType.ToString()}
                                            }
                                        );
                                    }

                                    JsonDocument doc;
                                    try
                                    {
                                        self.Logger?.LogDebug($"Received {json.Length} bytes, parsing JSON response...");
                                        doc = JsonDocument.Parse(json);
                                    }
                                    catch (JsonException e)
                                    {
                                        self.Logger?.LogError(e, "Failed to parse JSON.");
                                        throw new NetOpnApiInvalidResponseException(json);
                                    }

                                    if (doc.RootElement.ValueKind == JsonValueKind.Null)
                                    {
                                        self.Logger?.LogError("API returned NULL.");
                                        throw new NetOpnApiInvalidResponseException(ErrorCode.MissingResponseRootObject, "API returned NULL.");
                                    }

                                    var root = self.GetResponseRootElement(doc);
                                    try
                                    {
                                        self.Logger?.LogDebug("Processing response object...");
                                        setValue(root);
                                    }
                                    catch (ArgumentException e)
                                    {
                                        self.Logger?.LogError(e, "Failed to set the response.");
                                        throw new NetOpnApiInvalidResponseException(e);
                                    }
                                }

                                break;

                            default:
                                self.Logger?.LogError($"API returned unexpected status code {(int) response.StatusCode} ({response.StatusCode}).");
                                throw new NetOpnApiHttpException(response.StatusCode);
                        }
                    }
                }
            }
            catch (AggregateException e) when (e.InnerExceptions.First() is NetOpnApiException ex)
            {
                throw ex;
            }
            catch (AggregateException e) when (e.InnerExceptions.First() is HttpRequestException ex)
            {
                self.Logger?.LogError(ex, "HTTP request failed.");
                throw new NetOpnApiHttpException(ex);
            }
            catch (HttpRequestException e)
            {
                self.Logger?.LogError(e, "HTTP request failed.");
                throw new NetOpnApiHttpException(e);
            }
        }

        #endregion

        #region Execute

        /// <summary>
        /// Execute the command on the device.
        /// </summary>
        /// <exception cref="ArgumentNullException">Config is null. -or- ParameterSet is null.</exception>
        /// <exception cref="ArgumentException">Config is invalid. -or- ParameterSet is invalid.</exception>
        /// <exception cref="NetOpnApiHttpException">An HTTP error is raised or an invalid HTTP status code is returned.</exception>
        /// <exception cref="NetOpnApiNotAuthorizedException">The configured API key does not have sufficient access.</exception>
        /// <exception cref="NetOpnApiNotImplementedException">The command is not implemented on the device.</exception>
        /// <exception cref="NetOpnApiInvalidResponseException">The command returns an invalid response.</exception>
        public static void Execute(this ICommand self, int timeout = 100)
            => ExecuteCommand(
                self,
                self.CreateRequest,
                self.SetEmptyResponse,
                self.SetResponse,
                timeout
            );

        /// <summary>
        /// Execute the command on the device.
        /// </summary>
        /// <exception cref="ArgumentNullException">Config is null. -or- ParameterSet is null.</exception>
        /// <exception cref="ArgumentException">Config is invalid. -or- ParameterSet is invalid.</exception>
        /// <exception cref="NetOpnApiHttpException">An HTTP error is raised or an invalid HTTP status code is returned.</exception>
        /// <exception cref="NetOpnApiNotAuthorizedException">The configured API key does not have sufficient access.</exception>
        /// <exception cref="NetOpnApiNotImplementedException">The command is not implemented on the device.</exception>
        /// <exception cref="NetOpnApiInvalidResponseException">The command returns an invalid response.</exception>
        public static void Execute<TResponse>(this ICommandWithResponse<TResponse> self, int timeout = 100)
            => ExecuteCommand(
                self,
                self.CreateRequest,
                self.SetEmptyResponse,
                self.SetResponse,
                timeout
            );

        /// <summary>
        /// Execute the command on the device.
        /// </summary>
        /// <exception cref="ArgumentNullException">Config is null. -or- ParameterSet is null.</exception>
        /// <exception cref="ArgumentException">Config is invalid. -or- ParameterSet is invalid.</exception>
        /// <exception cref="NetOpnApiHttpException">An HTTP error is raised or an invalid HTTP status code is returned.</exception>
        /// <exception cref="NetOpnApiNotAuthorizedException">The configured API key does not have sufficient access.</exception>
        /// <exception cref="NetOpnApiNotImplementedException">The command is not implemented on the device.</exception>
        /// <exception cref="NetOpnApiInvalidResponseException">The command returns an invalid response.</exception>
        public static void Execute(this ICommandWithParameterSet self, int timeout = 100)
            => ExecuteCommand(
                self,
                self.CreateRequest,
                self.SetEmptyResponse,
                self.SetResponse,
                timeout
            );

        /// <summary>
        /// Execute the command on the device.
        /// </summary>
        /// <exception cref="ArgumentNullException">Config is null. -or- ParameterSet is null.</exception>
        /// <exception cref="ArgumentException">Config is invalid. -or- ParameterSet is invalid.</exception>
        /// <exception cref="NetOpnApiHttpException">An HTTP error is raised or an invalid HTTP status code is returned.</exception>
        /// <exception cref="NetOpnApiNotAuthorizedException">The configured API key does not have sufficient access.</exception>
        /// <exception cref="NetOpnApiNotImplementedException">The command is not implemented on the device.</exception>
        /// <exception cref="NetOpnApiInvalidResponseException">The command returns an invalid response.</exception>
        public static void Execute<TResponse>(this ICommandWithResponseAndParameterSet<TResponse> self, int timeout = 100)
            => ExecuteCommand(
                self,
                self.CreateRequest,
                self.SetEmptyResponse,
                self.SetResponse,
                timeout
            );

        #endregion

        #region Try Execute

        /// <summary>
        /// Try to executed the command.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="errorCode">Returns the error code if command execution fails.</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static bool TryExecute(this ICommand self, out ErrorCode errorCode, int timeout = 100)
        {
            errorCode = ErrorCode.NoError;

            try
            {
                self.Execute(timeout);
                return true;
            }
            catch (NetOpnApiException e)
            {
                errorCode = e.Code;
                return false;
            }
        }

        /// <summary>
        /// Try to executed the command.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="errorCode">Returns the error code if command execution fails.</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static bool TryExecute<TResponse>(this ICommandWithResponse<TResponse> self, out ErrorCode errorCode, int timeout = 100)
        {
            errorCode = ErrorCode.NoError;

            try
            {
                self.Execute(timeout);
                return true;
            }
            catch (NetOpnApiException e)
            {
                errorCode = e.Code;
                return false;
            }
        }

        /// <summary>
        /// Try to executed the command.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="errorCode">Returns the error code if command execution fails.</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static bool TryExecute(this ICommandWithParameterSet self, out ErrorCode errorCode, int timeout = 100)
        {
            errorCode = ErrorCode.NoError;

            try
            {
                self.Execute(timeout);
                return true;
            }
            catch (NetOpnApiException e)
            {
                errorCode = e.Code;
                return false;
            }
        }

        /// <summary>
        /// Try to executed the command.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="errorCode">Returns the error code if command execution fails.</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static bool TryExecute<TResponse>(this ICommandWithResponseAndParameterSet<TResponse> self, out ErrorCode errorCode, int timeout = 100)
        {
            errorCode = ErrorCode.NoError;

            try
            {
                self.Execute(timeout);
                return true;
            }
            catch (NetOpnApiException e)
            {
                errorCode = e.Code;
                return false;
            }
        }

        #endregion

        #region ExecuteAndWait

        /// <summary>
        /// Execute the command and wait for completion.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="perRequestTimeout"></param>
        /// <returns></returns>
        public static StatusWithUuidAndLog ExecuteAndWait(this ICommandWithResponse<StatusWithUuid> self, int perRequestTimeout = 10)
        {
            self.Execute(perRequestTimeout);
            if (self.Response?.Status != "ok")
            {
                return new StatusWithUuidAndLog()
                {
                    Status = self.Response?.Status,
                    Uuid   = self.Response?.Uuid ?? Guid.Empty
                };
            }

            var ret = new StatusWithUuidAndLog()
            {
                Status = "running",
                Uuid   = self.Response.Uuid
            };

            var progressCommand = new Commands.Core.Firmware.GetUpgradeProgress()
            {
                Config = self.Config,
                Logger = self.Logger
            };

            while (true)
            {
                progressCommand.Execute(perRequestTimeout);

                if (progressCommand.Response is null) throw new NetOpnApiInvalidResponseException(ErrorCode.MissingResponse, "Failed to determine status of running command.");

                switch (progressCommand.Response.Status)
                {
                    case "done":
                    case "reboot":
                        ret.Status = progressCommand.Response.Status;
                        ret.Log    = progressCommand.Response.Log;
                        return ret;

                    case "error":
                        ret.Status = "error";
                        ret.Log    = "There is no command running.";
                        return ret;

                    case "running":
                        Thread.Sleep(250);
                        break;

                    default:
                        throw new NetOpnApiInvalidResponseException(ErrorCode.InvalidStatus, $"The status \"{progressCommand.Response.Status}\" is not valid.");
                }
            }
        }

        #endregion
    }
}
