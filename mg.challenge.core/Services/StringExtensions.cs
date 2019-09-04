using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Core.Services
{
    public static class StringExtensions
    {
        public static string Clean(this string baseString)
        {
            baseString = baseString.Trim();

            if (baseString.StartsWith("\""))
                baseString = baseString.Substring(1);

            if (baseString.EndsWith("\""))
                baseString = baseString.Substring(0, baseString.Length - 1);

            return baseString;
        }
    }
}
