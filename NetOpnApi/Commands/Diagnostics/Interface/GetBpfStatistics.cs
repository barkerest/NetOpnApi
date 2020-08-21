using NetOpnApi.Models.Diagnostics.Interface;

namespace NetOpnApi.Commands.Diagnostics.Interface
{
    public class GetBpfStatistics : BaseCommand, ICommandWithResponse<BpfStatistics>
    {
        /// <inheritdoc />
        public BpfStatistics Response { get; set; }

        /// <inheritdoc />
        public override string ResponseRootElementName { get; } = "bpf-statistics";
    }
}
