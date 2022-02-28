using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommonInformation
{
    public static class DateTimeFormat
    {
        public static string ConvertToDDMMYYYY(DateTime dateTime)
        {
            return dateTime.ToString("MM-dd-yyyy");
        }
    }
}