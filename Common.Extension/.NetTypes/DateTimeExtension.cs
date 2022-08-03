using System;
using System.Collections.Generic;
using System.Text;

namespace ArabDT.Framwork.NetTypes
{
  public static class DateTimeExtension
    {
        public static DateTime NextDayOfWeek(this DateTime from, DayOfWeek dayOfWeek)
        {
          return  from.AddDays(((int)dayOfWeek - (int)from.DayOfWeek + 7) % 7);
        }
    }
}
