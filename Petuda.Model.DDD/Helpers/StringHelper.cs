using System;
using System.Text.RegularExpressions;

namespace Petuda.Model.DDD.Helpers
{
    public static class StringHelper
    {
        private static readonly char[] _delimiterChars;

        static StringHelper()
        {
            _delimiterChars = new[] { ' ', ',' };
        }

        public static char[] DelimiterChars
        {
            get { return _delimiterChars; }
        }

        public static string Trim(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return String.Empty;
            }
            return input.TrimStart().TrimEnd();
        }

        public static String[] Split(string input)
        {
            //remove redundant spaces
            input = Regex.Replace(Trim(input), @"\s+", " ");

            return input.Split(DelimiterChars);
        }
    }
}