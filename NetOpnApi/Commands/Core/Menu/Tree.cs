using System.Text.Json;
using NetOpnApi.Models.Core.System;
using NetOpnApi.Models.Core.System.Menu;

namespace NetOpnApi.Commands.Core.Menu
{
    /// <summary>
    /// Get the menu tree from the device.
    /// </summary>
    public class Tree : BaseCommand, ICommandWithResponse<TreeEntry[]>
    {
        /// <inheritdoc />
        public override JsonValueKind ResponseRootElementValueKind { get; } = JsonValueKind.Array;

        /// <inheritdoc />
        public TreeEntry[] Response { get; set; }
    }
}
