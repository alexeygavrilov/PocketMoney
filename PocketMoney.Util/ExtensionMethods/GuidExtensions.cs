using System;
using System.Diagnostics;
using PocketMoney.Util.Encoding;

namespace PocketMoney.Util.ExtensionMethods
{
    [DebuggerStepThrough]
    public static class GuidExtensions
    {
        private const string EQ = "=";
        private const char PLUS = '+';
        private const char MINUS = '-';
        private const char BACKSLASH = '/';
        private const char UNDERSCORE = '_';

        public static bool IsEmpty(this Guid instance)
        {
            return instance == Guid.Empty;
        }

        public static string ToBase64Url(this Guid guid)
        {
            return
                Convert.ToBase64String(guid.ToByteArray()).Replace(EQ, String.Empty).Replace(PLUS, MINUS).Replace(
                    BACKSLASH, UNDERSCORE);
        }


        public static Guid ToGuidFromBase64Url(this string guidstr)
        {
            return
                new Guid(Convert.FromBase64String(guidstr.Replace(MINUS, PLUS).Replace(UNDERSCORE, BACKSLASH) + EQ + EQ));
        }

        public static string ToBase32Url(this Guid guid)
        {
            return Base32Url.ToBase32String(guid.ToByteArray());
        }

        public static Guid FromBase32Url(this string guidstr)
        {
            return new Guid(Base32Url.FromBase32String(guidstr));
        }

        public static bool TryCreateFromBase32Url(this string guidstr, out Guid guid)
        {
            guid = Guid.Empty;
            try
            {
                guid = FromBase32Url(guidstr);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool TryParseGuidFromBase32Url(this string guidstr, out Guid guid)
        {
            guid = Guid.Empty;
            return Guid.TryParse(guidstr, out guid) || guidstr.TryCreateFromBase32Url(out guid);
        }
    }
}