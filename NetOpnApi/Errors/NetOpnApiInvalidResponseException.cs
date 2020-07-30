using System;

namespace NetOpnApi.Errors
{
    /// <summary>
    /// The API encountered an error processing the HTTP response.
    /// </summary>
    public class NetOpnApiInvalidResponseException : NetOpnApiException
    {
        internal NetOpnApiInvalidResponseException(string invalidJson)
            : base(ErrorCode.InvalidJsonResponse, "The response was not valid JSON.", invalidJson)
        {
            
        }

        internal NetOpnApiInvalidResponseException(ArgumentException error)
            : base(ErrorCode.InvalidResponseObject, "Failed to convert the JSON object into a valid response.", error.Message)
        {
            
        }

        internal NetOpnApiInvalidResponseException(ErrorCode code, string message)
            : base(code, message)
        {
            
        }
    }
}
