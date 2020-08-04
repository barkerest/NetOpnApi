using System;
using System.Collections.Generic;

namespace NetOpnApi.Models.Core.Menu
{
    /// <summary>
    /// The parameters for a menu search.
    /// </summary>
    public class SearchParameterSet : IParameterSet
    {
        /// <summary>
        /// Get/set the search term.
        /// </summary>
        public string SearchTerm { get; set; }
        
        IReadOnlyList<string> IParameterSet.GetUrlParameters() => null;

        IReadOnlyList<KeyValuePair<string, string>> IParameterSet.GetQueryParameters()
            => new[]
            {
                new KeyValuePair<string, string>("q", SearchTerm),
            };

        object IParameterSet.GetRequestPayload() => null;

        Type IParameterSet.GetRequestPayloadDataType() => null;
    }
}
