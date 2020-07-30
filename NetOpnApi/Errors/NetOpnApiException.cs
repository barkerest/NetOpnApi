using System;

namespace NetOpnApi.Errors
{
    /// <summary>
    /// An error raised by the NetOpnApi library.
    /// </summary>
    public class NetOpnApiException : ApplicationException
    {
        /// <summary>
        /// The error code.
        /// </summary>
        public ErrorCode Code { get; }
        
        /// <summary>
        /// Details relevant to the error.
        /// </summary>
        public string Details { get; }

        internal NetOpnApiException(ErrorCode code, string message)
            : base(message)
        {
            Code = code;
            Details = "";
        }

        internal NetOpnApiException(ErrorCode code, string message, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
            Details = "";
        }

        internal NetOpnApiException(ErrorCode code, string message, string details)
            : base(message)
        {
            Code = code;
            Details = details ?? "";
        }

        internal NetOpnApiException(ErrorCode code, string message, string details, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
            Details = details ?? "";
        }
    }
    
    
}
