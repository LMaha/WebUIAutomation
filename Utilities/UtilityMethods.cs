using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utilities
{
    public class UtilityMethods
    {
		StringComparison compareSetting = StringComparison.OrdinalIgnoreCase;

        public UtilityMethods()
        {

        }

        public double FindDecimalInString(string text)
        {
            return Convert.ToDouble(Regex.Split(text, @"[^0-9\.]+").Where(c => c != "." && c.Trim() != "").ToArray()[0]);
        }

		public long FindLongInString(string text)
		{
			return Convert.ToInt64(Regex.Match(text, @"\d+").Value);
		}

		public bool ContainsStrCaseInsensitive(string source, string toCheck)
		{
			return source != null && toCheck != null && source.IndexOf(toCheck, compareSetting) >= 0;
		}

		// Assumes only one set of parentheses
		public string GetTextBetweenParentheses(string text)
		{
			return Regex.Match(text, @"\(([^)]*)\)").Groups[1].Value;
		}
    }
}
