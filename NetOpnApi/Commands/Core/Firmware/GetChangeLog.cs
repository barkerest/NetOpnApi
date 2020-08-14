using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get change log text.
    /// </summary>
    /// <remarks>
    ///POST: /api/core/firmware/changelog/$version
    /// </remarks>
    public class GetChangeLog : BaseCommand, ICommandWithResponseAndParameterSet<ChangeLog, GetChangeLogParameterSet>
    {
        /// <inheritdoc />
        public ChangeLog                Response     { get; set; }

        /// <inheritdoc />
        public GetChangeLogParameterSet ParameterSet { get; } = new GetChangeLogParameterSet();

        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        public GetChangeLog()
            : base("changelog")
        {
            
        }
    }
}
