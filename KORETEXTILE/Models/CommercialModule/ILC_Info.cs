using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommercialModule
{
    public class ILC_Info
    {
        public string Gr { get; set; }
        public int ItemId { get; set; }
        public string NM { get; set; }
        public decimal QT { get; set; }
        public decimal PrvQt { get; set; }
        public decimal AvlQt { get; set; }
        public string UM { get; set; }
        public decimal LcrnQTY { get; set; }
        public int LCRNO { get; set; }
    }
}