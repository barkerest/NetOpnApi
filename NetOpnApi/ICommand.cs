using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace NetOpnApi
{
    /// <summary>
    /// The interface for an API command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Get the name of the module the command belongs to.
        /// </summary>
        public string Module { get; }

        /// <summary>
        /// Get the name of the controller containing the command.
        /// </summary>
        public string Controller { get; }

        /// <summary>
        /// Get the name of the command.
        /// </summary>
        public string Command { get; }

        /// <summary>
        /// Get or set the device configuration. 
        /// </summary>
        public IDeviceConfig Config { get; set; }

        /// <summary>
        /// Get or set the logger for the command to use.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Set to true to use POST otherwise GET will be used.
        /// </summary>
        public bool UsePost { get; }

        /// <summary>
        /// Get the name of the root element that contains the response in the JSON object. 
        /// </summary>
        public string ResponseRootElementName { get; }

        /// <summary>
        /// Get the type of root element that contains the response in the JSON object.
        /// </summary>
        public JsonValueKind ResponseRootElementValueKind { get; }
    }

    /// <summary>
    /// The interface for an API command with a response.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface ICommandWithResponse<TResponse> : ICommand
    {
        /// <summary>
        /// Get the response from command execution.
        /// </summary>
        public TResponse Response { get; set; }
    }

    public interface ICommandWithParameterSet : ICommand
    {
        /// <summary>
        /// Get a list of parameters to add to the request URL.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<string> GetUrlParameters();

        /// <summary>
        /// Get a list of key/value parameters to add to the request URL.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<KeyValuePair<string, string>> GetQueryParameters();
        
        /// <summary>
        /// Get an object to pass as the request payload. 
        /// </summary>
        /// <returns></returns>
        public object GetRequestPayload();

        /// <summary>
        /// Get the data type of the object to pass as the request payload (for serialization).
        /// </summary>
        /// <returns></returns>
        public Type GetRequestPayloadDataType();
    }

    /// <summary>
    /// The interface for an API command with a response and parameter set.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface ICommandWithResponseAndParameterSet<TResponse> : ICommandWithResponse<TResponse>, ICommandWithParameterSet
    {
    }
}
