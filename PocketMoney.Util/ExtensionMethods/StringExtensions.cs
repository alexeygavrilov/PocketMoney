using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace PocketMoney.Util.ExtensionMethods
{
    public static class StringExtensions
    {
        [DebuggerStepThrough]
        public static string Interpolate(this string instance, params object[] args)
        {
            return string.Format(CultureInfo.CurrentUICulture, instance, args);
        }

        [DebuggerStepThrough]
        public static string ClearSeparatorChars(this string instance)
        {
            return instance.Replace(Path.DirectorySeparatorChar, '\0');
        }

        public static string ToHex(this string instance)
        {
            return string.Concat(System.Text.Encoding.UTF8.GetBytes(instance).Select(b => b.ToString("X2")).ToArray());
        }

        public static string RemoveInvalidFileNameCharacters(this string instance,char replaceWith)
        {
            instance = Path.GetInvalidPathChars().Aggregate(instance, (current, invalidPathChar) => current.Replace(invalidPathChar, replaceWith));
            return Path.GetInvalidFileNameChars().Aggregate(instance, (current, invalidPathChar) => current.Replace(invalidPathChar, replaceWith));
 
        }

        public static string FromHex(this string instance)
        {
            byte[] list = new byte[instance.Length / 2];
            for (int i = 0; i < instance.Length; i += 2)
                list[i / 2] = Convert.ToByte(string.Concat(instance[i], instance[i + 1]), 16);
            return System.Text.Encoding.UTF8.GetString(list);

        }

        public static string ClearPunctuations(this string instance)
        {
            string result = string.Empty;
            if (!String.IsNullOrWhiteSpace(instance))
                foreach (char c in instance)
                    if (c >= '0' && c <= '9')
                        result += c;
            return result;
        }

        public static int ToInt32(this string instance)
        {
            int result = 0;
            int.TryParse(instance, out result);
            return result;
        }

        public static long ToInt64(this string instance)
        {
            long result = 0;
            long.TryParse(instance, out result);
            return result;
        }

        public static string Generation(this string characters, int length)
        {
            char[] chars = new char[length];
            Random rd = new Random();

            for (int i = 0; i < length; i++)
                chars[i] = characters[rd.Next(0, characters.Length)];

            return new string(chars);
        }




    }
}