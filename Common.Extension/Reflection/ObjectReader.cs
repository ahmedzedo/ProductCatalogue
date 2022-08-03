using System;
using System.Collections.Generic;
using System.Text;

namespace ArabDT.Framwork.Reflection
{
    public static class ObjectReader
    {
        public static string GetValueForCompainedFields(string[] filedNameArArray, object x)
        {
            //var filedNameArArray = filedsName.Split(',');
            StringBuilder result = new();

            foreach (var filed in filedNameArArray)
            {
                result.Append((string)x.GetType().GetProperty(filed.Trim()).GetValue(x, null));
                result.Append(' ');
            }

            return result.ToString();
        }
    }
}
