using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommonInformation
{
    [Table("InventoryTransaction")]
    public class InventoryTransaction
    {
        public InventoryTransaction(int ciid, decimal qty, decimal rate, decimal amount, int trtype, DateTime tdate, int storeId, int companyID,  string docType, int docID)
        {

            CIID = ciid;
            Qty = qty;
            Rate = rate; 
            Amount =amount;
            TRType = trtype;
            TDate = tdate;
            StoreID = storeId;
            CompanyID = companyID;
            DocType = docType;
            DocID = docID;
         
        }
        public InventoryTransaction()
        {

        }

        public Int64 ID { set; get; }
        public int CIID { set; get; }
        public decimal Qty { set; get; }
        public decimal Rate { set; get; }
        public decimal Amount { set; get; }
        public int TRType { set; get; }
        public DateTime TDate { set; get; }
        public int StoreID { set; get; }
        public int CompanyID { set; get; }
        public string DocType { set; get; }
        public int DocID { set; get; }
        [NotMapped]
        public int DepotID { set; get; }
        [NotMapped]
        public string GroupName { set; get; }
        [NotMapped]
        public string ItemName { set; get; }
    }
}