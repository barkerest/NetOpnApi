using System;
using System.Linq;
using System.Text.RegularExpressions;
using NetOpnApiBuilder.Enums;

namespace NetOpnApiBuilder.Extensions
{
    public static class NameExtensions
    {
        private static readonly Regex NonSafeChars = new Regex(@"[^a-zA-Z0-9_]+");
        private static readonly Regex WordSplitter = new Regex(@"(?:^[A-Z_]*|[A-Z_]+)[a-z0-9]*");

        public static bool IsSafeClrName(this string self, bool allowDots = false, bool allowDashes = false)
        {
            return IsSafeClrName(self, out _, allowDots, allowDashes);
        }

        public static bool IsSafeClrName(this string self, out string reason, bool allowDots = false, bool allowDashes=false)
        {
            if (string.IsNullOrEmpty(self))
            {
                reason = "can't be blank";
                return false;
            }

            var safeSelf = self;
            if (allowDots) safeSelf   = safeSelf.Replace('.', '_');
            if (allowDashes) safeSelf = safeSelf.Replace('-', '_');

            // can't have unsafe characters.
            if (NonSafeChars.IsMatch(safeSelf))
            {
                reason = "contains unsafe characters";
                return false;
            }

            // don't allow it to start with a number or underscore.
            var ch = self.First();
            if (ch == '_')
            {
                reason = "starts with underscore";
                return false;
            }

            if (ch == '.')
            {
                reason = "starts with dot";
                return false;
            }

            if (ch == '-')
            {
                reason = "starts with dash";
                return false;
            }

            if (ch >= '0' &&
                ch <= '9')
            {
                reason = "starts with number";
                return false;
            }

            ch = self.Last();
            if (ch == '_')
            {
                reason = "ends with underscore";
                return false;
            }

            if (ch == '.')
            {
                reason = "ends with dot";
                return false;
            }

            if (ch == '-')
            {
                reason = "ends with dash";
                return false;
            }

            reason = "";
            return true;
        }

        public static string ToSafeClrName(this string self)
        {
            if (string.IsNullOrWhiteSpace(self)) return "";
            self = self.Trim();
            self = NonSafeChars.Replace(self, "_");
            return string.Join(
                "",
                WordSplitter
                    .Matches(self)
                    .Select(x => x.Value.TrimStart('_'))
                    .Select(
                        x =>
                            (x.Length <= 1)
                                ? x.ToUpper()
                                : (x.Substring(0, 1).ToUpper() + x.Substring(1).ToLower())
                    )
            );
        }

        public static string ToSpacedName(this string self, bool titleCase = true)
        {
            if (string.IsNullOrWhiteSpace(self)) return "";
            self = self.Trim();
            self = NonSafeChars.Replace(self, "_");
            return string.Join(
                " ",
                WordSplitter
                    .Matches(self)
                    .Select(x => x.Value.TrimStart('_'))
                    .Select(
                        x =>
                            titleCase
                                ? (x.Length <= 1)
                                      ? x.ToUpper()
                                      : (x.Substring(0, 1).ToUpper() + x.Substring(1).ToLower())
                                : x.ToLower()
                    )
            );
        }

        public static string ToName(this ApiDataType self, bool nullable = false, bool titleCase = false)
        {
            return (nullable ? titleCase ? "Nullable " : "nullable " : "") + self.ToString().ToSpacedName(titleCase);
        }

        public static string ToShortName(this ApiDataType self, bool nullable = false)
        {
            var baseType = (self & ApiDataType.Object).ToName(false, false).Split(' ')[0];

            if ((self & ApiDataType.ArrayOfStrings) == ApiDataType.ArrayOfStrings)
            {
                return baseType +
                       (nullable
                           ? "[]?"
                           : "[]");
            }

            if ((self & ApiDataType.DictionaryOfStrings) == ApiDataType.DictionaryOfStrings)
            {
                return baseType +
                       (nullable
                           ? "{}?"
                           : "{}");
            }

            return nullable
                       ? (baseType + "?")
                       : baseType;
        }
    }
}
