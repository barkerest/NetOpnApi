using System;
using System.Collections.Generic;

namespace NetOpnApi
{
    /// <summary>
    /// A common interface for parameter sets.
    /// </summary>
    public interface IParameterSet
    {
        /// <summary>
        /// Get a list of parameters to add to the request URL.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<string> GetUrlParameters();
        
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
}
