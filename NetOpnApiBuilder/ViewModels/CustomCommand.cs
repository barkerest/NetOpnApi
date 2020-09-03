using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using NetOpnApi;
using NetOpnApiBuilder.Enums;
using NetOpnApiBuilder.Models;

namespace NetOpnApiBuilder.ViewModels
{
    public class CustomCommand : ICommandWithResponseAndParameterSet<JsonElement>
    {
        private readonly string[]                   _urlKeys;
        private readonly Dictionary<string, string> _urlParams;
        private readonly Dictionary<string, string> _queryMap;
        private readonly Dictionary<string, string> _queryParams;
        private          JsonElement?               _postBody;

        public CustomCommand(ApiCommand cmd)
        {
            Module     = cmd.Controller.Module.ApiName.ToLower();
            Controller = cmd.Controller.ApiName.ToLower();
            Command    = cmd.ApiName;
            UsePost    = cmd.UsePost;

            ResponseRootElementName = cmd.ResponseBodyPropertyName;

            if (cmd.UrlParams.Any())
            {
                _urlKeys   = cmd.UrlParams.OrderBy(x => x.Order).Select(x => x.ClrName).ToArray();
                _urlParams = cmd.UrlParams.ToDictionary(x => x.ClrName, _ => (string) null);
            }
            else
            {
                _urlKeys   = null;
                _urlParams = null;
            }

            if (cmd.QueryParams.Any())
            {
                _queryParams = cmd.QueryParams.ToDictionary(x => x.ClrName, _ => (string) null);
                _queryMap    = cmd.QueryParams.ToDictionary(x => x.ClrName, x => x.ApiName);
            }
            else
            {
                _queryMap    = null;
                _queryParams = null;
            }
        }

        public string        Module                       { get; }
        public string        Controller                   { get; }
        public string        Command                      { get; }
        public IDeviceConfig Config                       { get; set; }
        public ILogger       Logger                       { get; set; }
        public bool          UsePost                      { get; }
        public string        ResponseRootElementName      { get; }
        public JsonValueKind ResponseRootElementValueKind { get; } = JsonValueKind.Null;
        public JsonElement   Response                     { get; set; }

        public void SetUrlParameter(string clrName, string value)
        {
            if (string.IsNullOrEmpty(clrName)) throw new ArgumentException("cannot be blank", nameof(clrName));
            if (!_urlParams.ContainsKey(clrName)) throw new ArgumentException("not a valid CLR name", nameof(clrName));
            _urlParams[clrName] = value;
        }

        public void SetQueryParameter(string clrName, string value)
        {
            if (string.IsNullOrEmpty(clrName)) throw new ArgumentException("cannot be blank", nameof(clrName));
            if (!_queryParams.ContainsKey(clrName)) throw new ArgumentException("not a valid CLR name", nameof(clrName));
            _queryParams[clrName] = value;
        }

        public void SetPostBody(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                _postBody = null;
            }
            else
            {
                try
                {
                    _postBody = JsonDocument.Parse(json).RootElement;
                }
                catch (JsonException)
                {
                    _postBody = null;
                    throw new ArgumentException("must be valid json", nameof(json));
                }
            }
        }
        
        public IReadOnlyList<string> GetUrlParameters()
        {
            if (_urlKeys is null) return null;

            var ret = new List<string>();
            foreach (var key in _urlKeys)
            {
                var v = _urlParams[key];
                if (v is null) break;
                ret.Add(v);
            }

            return ret;
        }

        public IReadOnlyList<KeyValuePair<string, string>> GetQueryParameters()
        {
            return _queryMap
                   ?.Select(x => new KeyValuePair<string, string>(x.Value, _queryParams[x.Key]))
                   .Where(x => !(x.Value is null))
                   .ToArray();
        }

        public object GetRequestPayload()
        {
            return _postBody;
        }

        public Type GetRequestPayloadDataType()
        {
            return _postBody?.GetType();
        }
    }
}
