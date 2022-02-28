using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommonInformation
{
    [Table("SPInventoryTransaction")]
    public class SPInventoryTransaction
    {
        public SPInventoryTransaction(int ciid, decimal qty, decimal rate, decimal amount, int trtype, DateTime tdate, int storeId, int companyID,int typeId, int boxId, string docType, int docID)
        {

            CIID = ciid;
            Qty = qty;
            Rate = rate;
            Amount = amount;
            TRType = trtype;
            TDate = tdate;
            StoreID = storeId;
            CompanyID = companyID;
            TypeId = typeId;
            BoxID = boxId;
            DocType = docType;
            DocID = docID;

        }
        public SPInventoryTransaction()
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
        public int TypeId { get; set; }
        public int BoxID { get; set; }
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
