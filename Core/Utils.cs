using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SS.SMS.Core
{
    public static class Utils
    {
        public static bool IsMobile(string val)
        {
            return Regex.IsMatch(val, @"^1[3456789]\d{9}$", RegexOptions.IgnoreCase);
        }

        private static int _randomSeq;
        public static int GetRandomInt(int minValue, int maxValue)
        {
            var ro = new Random(unchecked((int)DateTime.Now.Ticks));
            var retval = ro.Next(minValue, maxValue);
            retval += _randomSeq++;
            if (retval >= maxValue)
            {
                _randomSeq = 0;
                retval = minValue;
            }
            return retval;
        }
    }
}
