using System.Linq;
using System.Text.RegularExpressions;

namespace NetOpnApiBuilder.Extensions
{
    public static class ClrNameExtensions
    {
        private static readonly Regex NonSafeChars = new Regex(@"[^a-zA-Z0-9_]+");
        private static readonly Regex WordSplitter = new Regex(@"(?:^[A-Z_]*|[A-Z_]+)[a-z0-9]*");

        public static bool IsSafeClrName(this string self)
        {
            return IsSafeClrName(self, out _);
        }

        public static bool IsSafeClrName(this string self, out string reason)
        {
            if (string.IsNullOrEmpty(self))
            {
                reason = "can't be blank";
                return false;
            }

            // can't have unsafe characters.
            if (NonSafeChars.IsMatch(self))
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
    }
}
