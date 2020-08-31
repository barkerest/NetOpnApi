using System;
using System.Collections.Generic;
using NetOpnApi.Models.Cron.Settings;

namespace NetOpnApi.Commands.Cron.Settings
{
    public class SearchJobs : BaseCommand, ICommandWithResponseAndParameterSet<JobSearchResult>
    {
        /// <inheritdoc />
        public JobSearchResult Response { get; set; }


        public int CurrentPage { get; set; } = 1;

        public int ItemsPerPage { get; set; } = 7;

        public string SearchPhrase { get; set; } = null;


        IReadOnlyList<string> ICommandWithParameterSet.GetUrlParameters() => null;

        IReadOnlyList<KeyValuePair<string, string>> ICommandWithParameterSet.GetQueryParameters()
            => new[]
            {
                new KeyValuePair<string, string>("current", CurrentPage.ToString()),
                new KeyValuePair<string, string>("rowCount", ItemsPerPage.ToString()),
                new KeyValuePair<string, string>("searchPhrase", SearchPhrase),
            };

        object ICommandWithParameterSet.GetRequestPayload() => null;

        Type ICommandWithParameterSet.GetRequestPayloadDataType() => null;
    }
}
