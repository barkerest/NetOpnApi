using System.Text.Json;
using NetOpnApi.Models.Core.Menu;

namespace NetOpnApi.Commands.Core.Menu
{
    /// <summary>
    /// Get the menu tree from the device.
    /// </summary>
    /// <remarks>
    /// GET: /api/core/menu/tree
    /// </remarks>
    public class Tree : BaseCommand, ICommandWithResponse<TreeEntry[]>
    {
        /// <inheritdoc />
        public override JsonValueKind ResponseRootElementValueKind { get; } = JsonValueKind.Array;

        /// <inheritdoc />
        public TreeEntry[] Response { get; set; }
    }
}
