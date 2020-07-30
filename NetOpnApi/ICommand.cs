using System;
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

    /// <summary>
    /// The interface for an API command with a parameter set.
    /// </summary>
    /// <typeparam name="TParameterSet"></typeparam>
    public interface ICommandWithParameterSet<TParameterSet> : ICommand
        where TParameterSet : IParameterSet
    {
        /// <summary>
        /// Get the parameter set.
        /// </summary>
        public TParameterSet ParameterSet { get; }
    }

    /// <summary>
    /// The interface for an API command with a response and parameter set.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TParameterSet"></typeparam>
    public interface ICommandWithResponseAndParameterSet<TResponse, TParameterSet> : ICommandWithResponse<TResponse>, ICommandWithParameterSet<TParameterSet>
        where TParameterSet : IParameterSet
    {
    }
}
