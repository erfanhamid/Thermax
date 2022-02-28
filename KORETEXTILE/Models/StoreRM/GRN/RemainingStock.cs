using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BEEERP.Models.StoreRM.GRN
{
    [Table("RemainingStocks")]
    public class RemainingStock
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public decimal Qty { get; set; }
        public int LCRNNo { get; set; }
        public decimal UnitCost { get; set; }
        public string DocType { get; set; }
        public int DocNo { get; set; }
        public int StoreId { get; set; }
    }
}