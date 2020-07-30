using System.Text.Json;
using NetOpnApi.Models.Core.System;

namespace NetOpnApi.Commands.Core.Menu
{
    /// <summary>
    /// Get the menu tree from the device.
    /// </summary>
    public class Tree : BaseCommand, ICommandWithResponse<MenuTreeEntry[]>
    {
        /// <inheritdoc />
        public override JsonValueKind ResponseRootElementValueKind { get; } = JsonValueKind.Array;

        /// <inheritdoc />
        public MenuTreeEntry[] Response { get; set; }
    }
}
