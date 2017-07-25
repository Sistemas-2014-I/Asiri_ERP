using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class DateExt
    {
        public static string OnlyDate(this DateTime? dt)
        {
            return dt != null ? ((DateTime)dt).ToString("dd/MM/yyyy") : "";
        }

        public static string DateAndTime(this DateTime? dt)
        {
            return dt != null ? ((DateTime)dt).ToString("dd/MM/yyyy hh:mm:ss") : "";
        }


        public static string OnlyDate(this DateTime dt)
        {
            return dt.ToString("dd/MM/yyyy");
        }

        public static string DateAndTime(this DateTime dt)
        {
            return dt.ToString("dd/MM/yyyy hh:mm:ss");
        }
        public static string DateForCode(this DateTime dt)
        {
            int aleatorio = new Random().Next(0, 9);
            return dt.ToString("yyyyMMddhhmmssffff", CultureInfo.InvariantCulture) + aleatorio;
        }
    }
}