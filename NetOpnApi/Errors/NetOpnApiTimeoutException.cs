using System;

namespace NetOpnApi.Errors
{
    /// <summary>
    /// The request timed out.
    /// </summary>
    public class NetOpnApiTimeoutException : NetOpnApiException
    {
        public NetOpnApiTimeoutException()
            : base(ErrorCode.Timeout, "The request timed out.")
        {
            
        }

        public NetOpnApiTimeoutException(TimeSpan timeout)
            : base(ErrorCode.Timeout, $"The request timed out after {timeout}.")
        {
            
        }
    }
}
