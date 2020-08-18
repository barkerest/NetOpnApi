using System;
using System.Collections.Generic;

namespace NetOpnApi.Models.Cron.Settings
{
    public class JobSearchParameterSet : IParameterSet
    {
        public int    CurrentPage  { get; set; } = 1;
        public int    ItemsPerPage { get; set; } = 7;
        public string SearchPhrase { get; set; } = null;

        IReadOnlyList<string> IParameterSet.GetUrlParameters() => null;

        IReadOnlyList<KeyValuePair<string, string>> IParameterSet.GetQueryParameters()
            => new[]
            {
                new KeyValuePair<string, string>("current", CurrentPage.ToString()),
                new KeyValuePair<string, string>("rowCount", ItemsPerPage.ToString()),
                new KeyValuePair<string, string>("searchPhrase", SearchPhrase),
            };

        object IParameterSet.GetRequestPayload() => null;

        Type IParameterSet.GetRequestPayloadDataType() => null;
    }
}
