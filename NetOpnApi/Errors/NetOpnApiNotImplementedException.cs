using System;
using System.Net;

namespace NetOpnApi.Errors
{
    /// <summary>
    /// The API command is not implemented on the device.
    /// </summary>
    public class NetOpnApiNotImplementedException : NetOpnApiException
    {
        internal static readonly HttpStatusCode[] HandledCodes =
        {
            HttpStatusCode.PermanentRedirect,
            HttpStatusCode.BadRequest,
            HttpStatusCode.NotFound,
            HttpStatusCode.MethodNotAllowed,
            HttpStatusCode.NotImplemented
        };

        private static string GetDetailsFor(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.PermanentRedirect:
                    return "Command has moved.";

                case HttpStatusCode.BadRequest:
                    return "Command does not exist.";

                case HttpStatusCode.NotFound:
                    return "Command not found.";

                case HttpStatusCode.MethodNotAllowed:
                    return "Invalid verb.";

                case HttpStatusCode.NotImplemented:
                    return "Not implemented.";

                default:
                    throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, null);
            }
        }

        internal NetOpnApiNotImplementedException(HttpStatusCode statusCode, ICommand command)
            : base(
                ErrorCode.CommandNotImplemented,
                $"{command.Module}/{command.Controller}/{command.Command}",
                GetDetailsFor(statusCode)
            )
        {
        }
    }
}
