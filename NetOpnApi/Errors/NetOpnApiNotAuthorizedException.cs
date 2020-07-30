using System.Net;

namespace NetOpnApi.Errors
{
    /// <summary>
    /// The configured API key is not authorized to execute the action.
    /// </summary>
    public class NetOpnApiNotAuthorizedException : NetOpnApiException
    {
        internal static readonly HttpStatusCode[] HandledCodes =
        {
            HttpStatusCode.Forbidden,
            HttpStatusCode.Unauthorized,
        };
        
        internal NetOpnApiNotAuthorizedException(HttpStatusCode statusCode)
            : base(ErrorCode.NotAuthorizedForCommand, $"Server returned {statusCode}")
        {
            
        }
    }
}
