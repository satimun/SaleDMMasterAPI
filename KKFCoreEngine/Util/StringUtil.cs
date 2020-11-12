using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKFCoreEngine.Util
{
    public static class StringUtil
    {
        public static string GetStringValue(string val)
        {
            if(val != null && val != "") {
                val = val.Trim();
                if(val != "") { return val; }
            }
            return null;
        }

        public static string Join(string sp, List<string> val)
        {
            if (val != null) { return string.Join(sp, val); }
            return null;
        }

        public static string Join(string sp, List<int> val)
        {
            if (val != null) { return string.Join(sp, val); }
            return null;
        }
    }
}
