using System;
using System.Collections.Generic;
using System.Linq;

namespace NetOpnApi.Models.Cron.Settings
{
    public class AddJobParameterSet : AddUpdateJobRequest.Values, IParameterSet
    {
        IReadOnlyList<string> IParameterSet.GetUrlParameters() => null;

        IReadOnlyList<KeyValuePair<string, string>> IParameterSet.GetQueryParameters() => null;

        object IParameterSet.GetRequestPayload()
        {
            return new AddUpdateJobRequest()
            {
                Job =
                {
                    Origin      = Origin,
                    Enabled     = Enabled,
                    Command     = Command,
                    Parameters  = Parameters,
                    Description = Description,
                    Minutes     = Minutes,
                    Hours       = Hours,
                    Days        = Days,
                    Months      = Months,
                    Weekdays    = Weekdays,
                }
            };
        }

        Type IParameterSet.GetRequestPayloadDataType() => typeof(AddUpdateJobRequest);
    }
}
