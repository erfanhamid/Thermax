using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.StoreDepot
{
    public class InventoryVReport
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime AsOnDate { get; set; }
        public int DepotFrom { get; set; }
        public int StoreFrom { get; set; }
        public int DepotTo { get; set; }
        public int StoreTo { get; set; }
        public int Group { get; set; }
        public int Item { get; set; }
    }
}