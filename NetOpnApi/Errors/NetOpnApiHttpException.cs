using System.Net;
using System.Net.Http;

namespace NetOpnApi.Errors
{
    /// <summary>
    /// The API encountered an error processing the HTTP request.
    /// </summary>
    public class NetOpnApiHttpException : NetOpnApiException
    {
        internal NetOpnApiHttpException(HttpRequestException exception)
            : base(ErrorCode.HttpException, "The request generated an HTTP exception.", exception)
        {
            
        }

        internal NetOpnApiHttpException(HttpStatusCode statusCode)
            : base(ErrorCode.HttpStatusCode, $"The request returned HTTP status code {(int)statusCode} ({statusCode}).")
        {
            
        }
    }
}
