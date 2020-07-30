using System.Text.Json;
using NetOpnApi.Models.Core.System;

namespace NetOpnApi.Commands.Core.Menu
{
    /// <summary>
    /// Search the menu on the device for a specific search term.
    /// </summary>
    public class Search : BaseCommand, ICommandWithResponseAndParameterSet<MenuSearchEntry[], MenuSearchParameterSet>
    {
        /// <inheritdoc />
        public override JsonValueKind ResponseRootElementValueKind { get; } = JsonValueKind.Array;

        /// <inheritdoc />
        public MenuSearchEntry[] Response { get; set; }

        /// <inheritdoc />
        public MenuSearchParameterSet ParameterSet { get; } = new MenuSearchParameterSet();
    }
}
