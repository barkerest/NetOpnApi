namespace NetOpnApi
{
    /// <summary>
    /// The various error codes provided in API errors.
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// No error occurred.
        /// </summary>
        NoError,
        
        /// <summary>
        /// The HTTP client raised an exception.
        /// </summary>
        HttpException,
        
        /// <summary>
        /// The HTTP client returned an invalid status code.
        /// </summary>
        HttpStatusCode,
        
        /// <summary>
        /// The command is not implemented.
        /// </summary>
        CommandNotImplemented,
        
        /// <summary>
        /// The command did not return a JSON response.
        /// </summary>
        InvalidJsonResponse,
        
        /// <summary>
        /// The JSON response could not be decoded into the expected object.
        /// </summary>
        InvalidResponseObject,
        
        /// <summary>
        /// The JSON response did not contain the required root element.
        /// </summary>
        MissingResponseRootObject,
        
        /// <summary>
        /// The JSON response did not contain a root element of the correct type.
        /// </summary>
        InvalidResponseRootObject,
        
        /// <summary>
        /// The configured API key is not authorized to execute the command.
        /// </summary>
        NotAuthorizedForCommand,
        
        
    }
}
