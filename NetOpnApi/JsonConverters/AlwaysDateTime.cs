using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace NetOpnApi.JsonConverters
{
    /// <summary>
    /// The value is always a DateTime.  Will read from string and write to string.
    /// </summary>
    public class AlwaysDateTime : JsonConverter<DateTime>
    {
        private const string Year     = "YR";
        private const string Month    = "MO";
        private const string Day      = "DY";
        private const string Hour     = "HR";
        private const string Minute   = "MI";
        private const string Second   = "S";
        private const string Fraction = "F";
        private const string AmPm     = "AP";
        private const string Offset   = "OFF";

        private static readonly Dictionary<string, int> Months = new Dictionary<string, int>()
        {
            {"1", 1},
            {"01", 1},
            {"jan", 1},
            {"january", 1},
            {"2", 2},
            {"02", 2},
            {"feb", 2},
            {"february", 2},
            {"3", 3},
            {"03", 3},
            {"mar", 3},
            {"march", 3},
            {"4", 4},
            {"04", 4},
            {"apr", 4},
            {"april", 4},
            {"5", 5},
            {"05", 5},
            {"may", 5},
            {"6", 6},
            {"06", 6},
            {"jun", 6},
            {"june", 6},
            {"7", 7},
            {"07", 7},
            {"jul", 7},
            {"july", 7},
            {"8", 8},
            {"08", 8},
            {"aug", 8},
            {"august", 8},
            {"9", 9},
            {"09", 9},
            {"sep", 9},
            {"september", 9},
            {"10", 10},
            {"oct", 10},
            {"october", 10},
            {"11", 11},
            {"nov", 11},
            {"november", 11},
            {"12", 12},
            {"dec", 12},
            {"december", 12},
        };

        private const string TimeRegexPortion = @"(?<HR>\d{1,2}):(?<MI>\d{1,2})(?::(?<S>\d{1,2})(?:\.(?<F>\d+))?)?\s?(?<AP>[AP]M?)?\s?(?:Z|GMT|UTC|(?<OFF>(?:\+|-)?\d{1,2}:\d\d))?";

        // Straightforward YYYY-MM-DD format.
        private static readonly Regex UniversalSortable
            = new Regex($@"\A(?<YR>\d{{4}})-(?<MO>\d{{1,2}})-(?<DY>\d{{1,2}})(?:(?:\s|T){TimeRegexPortion})?\Z", RegexOptions.IgnoreCase);

        // OPNsense is sponsored by Decisio, based in the Netherlands.
        // It stands to reason that if we get any short dates from OPNsense, they will be in the Euro style.
        private static readonly Regex EuroStyle // D/M/YYYY, D.M.YYYY, D-M-YYYY
            = new Regex($@"\A(?<DY>\d{{1,2}})[\./-](?<MO>\d{{1,2}})[\./-](?<YR>\d{{4}})(?:\s{TimeRegexPortion})?\Z", RegexOptions.IgnoreCase);

        // Jan 1 2020 12:00:00 PM format.
        private static readonly Regex WrittenWithTimeAtEnd
            = new Regex($@"(?:\A|\s)(?<MO>[a-z]+)\s+(?<DY>\d{{1,2}}),?\s+(?<YR>\d{{4}})(?:,?\s{TimeRegexPortion})?\Z", RegexOptions.IgnoreCase);

        // Jan 1 12:00:00 PM 2020 format.
        private static readonly Regex WrittenWithTimeInside
            = new Regex($@"(?:\A|\s)(?<MO>[a-z]+)\s+(?<DY>\d{{1,2}})(?:,?\s{TimeRegexPortion})?,?\s+(?<YR>\d{{4}})\Z", RegexOptions.IgnoreCase);

        // 1 Jan 2020 12:00:00 PM format.
        private static readonly Regex WrittenEuroWithTimeAtEnd
            = new Regex($@"(?:\A|\s)(?<DY>\d{{1,2}})\s+(?<MO>[a-z]+),?\s+(?<YR>\d{{4}})(?:,?\s{TimeRegexPortion})?\Z", RegexOptions.IgnoreCase);

        // 1 Jan 12:00:00 PM 2020 format.
        private static readonly Regex WrittenEuroWithTimeInside
            = new Regex($@"(?:\A|\s)(?<DY>\d{{1,2}})\s+(?<MO>[a-z]+)(?:,?\s{TimeRegexPortion})?,?\s+(?<YR>\d{{4}})\Z", RegexOptions.IgnoreCase);


        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null) return DateTime.MinValue;
            if (reader.TokenType == JsonTokenType.Number &&
                reader.GetInt32() == 0) return DateTime.MinValue;

            var s = reader.GetString();
            if (string.IsNullOrEmpty(s)) return DateTime.MinValue;
            if (s == "0") return DateTime.MinValue;

            int year;
            int month;
            int day;
            var hour        = 0;
            var minute      = 0;
            var second      = 0;
            var millisecond = 0;

            var m             = UniversalSortable.Match(s);
            if (!m.Success) m = EuroStyle.Match(s);
            if (!m.Success) m = WrittenWithTimeAtEnd.Match(s);
            if (!m.Success) m = WrittenWithTimeInside.Match(s);
            if (!m.Success) m = WrittenEuroWithTimeAtEnd.Match(s);
            if (!m.Success) m = WrittenEuroWithTimeInside.Match(s);
            if (!m.Success) throw new JsonException("Failed to determine date format.");

            if (!int.TryParse(m.Groups[Year].Value, out year))
            {
                throw new JsonException("Failed to parse year.");
            }

            if (!Months.TryGetValue(m.Groups[Month].Value.ToLower(), out month))
            {
                throw new JsonException("Failed to parse month.");
            }

            if (!int.TryParse(m.Groups[Day].Value, out day))
            {
                throw new JsonException("Failed to parse day.");
            }

            if (m.Groups[Hour].Success &&
                !int.TryParse(m.Groups[Hour].Value, out hour))
            {
                throw new JsonException("Failed to parse hour.");
            }

            if (m.Groups[Minute].Success &&
                !int.TryParse(m.Groups[Minute].Value, out minute))
            {
                throw new JsonException("Failed to parse minute.");
            }

            if (m.Groups[Second].Success &&
                !int.TryParse(m.Groups[Second].Value, out second))
            {
                throw new JsonException("Failed to parse second.");
            }

            if (m.Groups[Fraction].Success)
            {
                var f               = m.Groups[Fraction].Value;
                if (f.Length > 3) f = f.Substring(0, 3);
                if (f.Length < 3) f = f.PadRight(3, '0');
                if (!int.TryParse(f, out millisecond))
                {
                    throw new JsonException("Failed to parse millisecond.");
                }
            }

            if (m.Groups[AmPm].Success)
            {
                if (hour < 1 ||
                    hour > 12)
                {
                    throw new JsonException("Hour value is invalid in AM/PM mode.");
                }

                switch (m.Groups[AmPm].Value.ToLower()[0])
                {
                    case 'p' when hour != 12:
                        hour += 12;
                        break;
                    case 'a' when hour == 12:
                        hour = 0;
                        break;
                }
            }

            var ret = new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Utc);

            if (m.Groups[Offset].Success)
            {
                var off                 = m.Groups[Offset].Value;
                var mult                = -1;
                if (off[0] == '-') mult = 1;
                var offPart             = off.TrimStart('-', '+').Split(':');
                var hourOff             = mult * int.Parse(offPart[0]);
                var minOff              = mult * int.Parse(offPart[1]);

                ret = ret.AddHours(hourOff).AddMinutes(minOff);
            }

            return ret;
        }


        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToUniversalTime().ToString("u"));
    }
}
