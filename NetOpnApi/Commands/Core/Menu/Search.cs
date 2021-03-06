﻿using System.Text.Json;
using NetOpnApi.Models.Core.Menu;

namespace NetOpnApi.Commands.Core.Menu
{
    /// <summary>
    /// Search the menu on the device for a specific search term.
    /// </summary>
    /// <remarks>
    /// GET: /api/core/menu/search
    /// </remarks>
    public class Search : BaseCommand, ICommandWithResponseAndParameterSet<SearchEntry[], SearchParameterSet>
    {
        /// <inheritdoc />
        public override JsonValueKind ResponseRootElementValueKind { get; } = JsonValueKind.Array;

        /// <inheritdoc />
        public SearchEntry[] Response { get; set; }

        /// <inheritdoc />
        public SearchParameterSet ParameterSet { get; } = new SearchParameterSet();
    }
}
