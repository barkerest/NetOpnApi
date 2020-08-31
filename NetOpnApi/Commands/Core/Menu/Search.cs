using System;
using System.Collections.Generic;
using System.Text.Json;
using NetOpnApi.Models.Core.Menu;

namespace NetOpnApi.Commands.Core.Menu
{
    /// <summary>
    /// Search the menu on the device for a specific search term.
    /// </summary>
    /// <remarks>
    /// GET: /api/core/menu/search
    /// </remarks>
    public class Search : BaseCommand, ICommandWithResponseAndParameterSet<SearchEntry[]>
    {
        /// <inheritdoc />
        public override JsonValueKind ResponseRootElementValueKind { get; } = JsonValueKind.Array;

        /// <inheritdoc />
        public SearchEntry[] Response { get; set; }

        /// <summary>
        /// Get/set the search term.
        /// </summary>
        public string SearchTerm { get; set; }
        
        IReadOnlyList<string> ICommandWithParameterSet.GetUrlParameters() => null;

        IReadOnlyList<KeyValuePair<string, string>> ICommandWithParameterSet.GetQueryParameters()
            => new[]
            {
                new KeyValuePair<string, string>("q", SearchTerm),
            };

        object ICommandWithParameterSet.GetRequestPayload() => null;

        Type ICommandWithParameterSet.GetRequestPayloadDataType() => null;
    }
}
