using System;

namespace BackEndTestApp.Extensions
{
    public static class StringExtensions
    {
        public static int ParseToInt(this string s)
        {
            int intValue;
            if (!int.TryParse(s, out intValue))
                throw new Exception("Incorrect string");
            return intValue;
        }
    }
}