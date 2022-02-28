using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Authentication
{
    public static class ApproverNotification
    {

        public static List<Tuple<string, string>> AllNotification()
        {
            List<Tuple<string, string>> items = new List<Tuple<string, string>>();
            items.Add(new Tuple<string, string>("Sales", "SalesEntry"));
            items.Add(new Tuple<string, string>("Order", "OrderEntry"));
            return items;
        }
    }
}