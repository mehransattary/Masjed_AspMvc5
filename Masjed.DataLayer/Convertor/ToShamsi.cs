using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
namespace Masjed.Utilites.Convertor
{
    public static class ToShamsi
    {
        public static string ConvertToShamsi(this string value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(Convert.ToDateTime(value)) + "/" + pc.GetMonth(Convert.ToDateTime(value)).ToString("00") + "/" + pc.GetDayOfMonth(Convert.ToDateTime(value)).ToString("00");
        }
        public static DateTime ConvertToShamsiDateTime(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            int Year = pc.GetYear(value);
            int Month = pc.GetMonth(value);
            int Day = pc.GetDayOfMonth(value);
            DateTime Date = Convert.ToDateTime((Year + "/" + Month + "/" + Day).ToString());
            return Date;
        }
    }
}
