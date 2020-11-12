using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Util
{
    public static class DateTimeUtil
    {
        static GregorianCalendar _gc = new GregorianCalendar();
        public static string GetDateTimeString(this System.DateTime? dt)
        {
            string dtx = "";
            if (dt.HasValue)
            {
                dtx += dt.Value.Year.ToString() + "-";
                dtx += (dt.Value.Month < 10 ? "0" + dt.Value.Month.ToString() : dt.Value.Month.ToString()) + "-";
                dtx += (dt.Value.Day < 10 ? "0" + dt.Value.Day.ToString() : dt.Value.Day.ToString());
                dtx += " ";
                dtx += (dt.Value.Hour < 10 ? "0" + dt.Value.Hour.ToString() : dt.Value.Hour.ToString()) + ":";
                dtx += (dt.Value.Minute < 10 ? "0" + dt.Value.Minute.ToString() : dt.Value.Minute.ToString()) + ":";
                dtx += (dt.Value.Second < 10 ? "0" + dt.Value.Second.ToString() : dt.Value.Second.ToString());
            }
            return dtx;
        }
        public static string GetDateString(this System.DateTime? d)
        {
            string dx = "";
            if (d.HasValue)
            {
                dx += d.Value.Year.ToString() + "-";
                dx += (d.Value.Month < 10 ? "0" + d.Value.Month.ToString() : d.Value.Month.ToString()) + "-";
                dx += (d.Value.Day < 10 ? "0" + d.Value.Day.ToString() : d.Value.Day.ToString());
            }
            return dx;
        }

        public static string GetDateViewString(this System.DateTime? d)
        {
            string dx = "";
            if (d.HasValue)
            {
                dx += (d.Value.Day < 10 ? "0" + d.Value.Day.ToString() : d.Value.Day.ToString()) + "/";
                dx += (d.Value.Month < 10 ? "0" + d.Value.Month.ToString() : d.Value.Month.ToString()) + "/";
                dx += d.Value.Year.ToString() ;
                  
            }
            return dx;
        }

        public static string GetDateTimeISO(this System.DateTime? dt)
        {
            string dx = "";
            if (dt.HasValue)
            {
                dx = dt.Value.ToUniversalTime().ToString("s") + "Z";
            }
            return dx;
        }

        public static string GetDateTimeISO(this DateTime dt)
        {
            string dx = "";
            dx = dt.ToUniversalTime().ToString("s") + "Z";
            return dx;
        }

        public static int GetWeekOfMonth(this System.DateTime time)
        {
            System.DateTime first = new System.DateTime(time.Year, time.Month, 1);
            return time.GetWeekOfYear() - first.GetWeekOfYear() + 1;
        }

        static int GetWeekOfYear(this System.DateTime time)
        {
            return _gc.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public static System.DateTime? GetDateTime(this string dt)
        {
            try
            {
                var tmp = dt.Trim().Split(' ');
                var d = tmp[0].Split('-');
                var t = tmp[1].Split(':');
                return new System.DateTime(int.Parse(d[0]), int.Parse(d[1]), int.Parse(d[2]), int.Parse(t[0]), int.Parse(t[1]), int.Parse(t[2]));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static System.DateTime? GetDate(this string d)
        {
            try
            {
                var tmp = d.Trim().Split('-');
                return new System.DateTime(int.Parse(tmp[0]), int.Parse(tmp[1]), int.Parse(tmp[2]));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static System.DateTime ChangeTime(this System.DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new System.DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds);
        }


        // datatime 2

        public static string ToStringDT(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ToStringViewDT(this DateTime? d)
        {
            string dx = "";
            if (d.HasValue)
            {
                dx += (d.Value.Day < 10 ? "0" + d.Value.Day.ToString() : d.Value.Day.ToString()) + "/";
                dx += (d.Value.Month < 10 ? "0" + d.Value.Month.ToString() : d.Value.Month.ToString()) + "/";
                dx += d.Value.Year.ToString()+" ";
                dx += (d.Value.Hour < 10 ? "0" + d.Value.Hour.ToString() : d.Value.Hour.ToString())  +":";
                dx += (d.Value.Minute < 10 ? "0" + d.Value.Minute.ToString() : d.Value.Minute.ToString())   + ":";
                dx += (d.Value.Second < 10 ? "0" + d.Value.Second.ToString() : d.Value.Second.ToString())    ;
            }
            return dx;
        }

        public static string ToStringD(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd 00:00:00");
        }

        public static long? GetNumber(this DateTime? dt)
        {
            if (dt.HasValue)
            {
                try
                {
                    JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                    };
                    string d = JsonConvert.SerializeObject(dt.Value.ToUniversalTime(), microsoftDateFormatSettings);
                    string tmp = "";
                    for (var i = 0; i < d.Length; i++)
                    {
                        if (Regex.Match(d[i].ToString(), @"[0-9]").Success)
                        {
                            tmp += d[i].ToString();
                        }
                    }
                    return Convert.ToInt64(tmp);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
        }

        public static long GetNumber(this DateTime dt)
        {
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                string d = JsonConvert.SerializeObject(dt.ToUniversalTime(), microsoftDateFormatSettings);
                string tmp = "";
                var xxx = dt.ToUniversalTime();
                for (var i = 0; i < d.Length; i++)
                {
                    if (Regex.Match(d[i].ToString(), @"[0-9]").Success)
                    {
                        tmp += d[i].ToString();
                    }
                }
                return Convert.ToInt64(tmp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static DateTime? NumberToDateTime(this Int64 dt)
        {
            try
            {
                DateTime obj = (DateTime)JsonConvert.DeserializeObject(@"""\/Date(" + dt + @")\/""", new JsonSerializerSettings() { DateParseHandling = DateParseHandling.DateTime });
                return obj.ToLocalTime();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public static string ToRefId(this DateTime? d)
        {
            string dx = "";
            if (d.HasValue)
            {
                dx += d.Value.Year.ToString() + "";
                dx += (d.Value.Month < 10 ? "0" + d.Value.Month.ToString() : d.Value.Month.ToString()) + "";
                dx += (d.Value.Day < 10 ? "0" + d.Value.Day.ToString() : d.Value.Day.ToString()) + "-";
                dx += (d.Value.Hour < 10 ? "0" + d.Value.Hour.ToString() : d.Value.Hour.ToString()) + "";
                dx += (d.Value.Minute < 10 ? "0" + d.Value.Minute.ToString() : d.Value.Minute.ToString()) + "";
                dx += (d.Value.Second < 10 ? "0" + d.Value.Second.ToString() : d.Value.Second.ToString());
                dx += d.Value.ToString("fff");
            }
            return dx;
        }

    }
}
