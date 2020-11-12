using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Util
{
    public static class NumberUtil
    {
        public static int? GetID(this int val)
        {
            if (val == 0) return null;
            return val;
        }

        public static int? GetID(this int? val)
        {
            if (val == 0) return null;
            return val;
        }

        public static int Strtoint(string s)
        {
            int _r = 0;


            if (s.Trim() == "")
            {
                _r = 0;
            }
            else
            {
                _r = Convert.ToInt32(s.Trim());
            }

            return _r;
        }


    }
}
