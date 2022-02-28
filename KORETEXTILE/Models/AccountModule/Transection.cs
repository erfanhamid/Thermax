using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.AccountModule
{
    [Table("Transection")]
    public class Transection
    {
        public Transection(int cAID, Single trType, decimal amount, Int64 transectionID, DateTime trdate, string referenceNo, string payeeName, string transectionDesc, string docType, int docID, int? costCntrID, int? costGrpID)
        {
           
            CAID = cAID;
            this.trType = trType;
            Amount = amount;
            TransectionID = transectionID;
            this.trdate = trdate;
            this.referenceNo = referenceNo;
            this.payeeName = payeeName;
            this.transectionDesc = transectionDesc;
            DocType = docType;
            DocID = docID;
            CostCntrID = costCntrID;
            CostGrpID = costGrpID;
        }
        public Transection()
        {

        }
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int CAID { get; set; }
        public Single trType { get; set; }
        public decimal Amount { get; set; }
        [Key]
        [Column(Order = 2)]
        public Int64 TransectionID { get; set; }
        public DateTime trdate { get; set; }
        public string referenceNo { get; set; }
        public string payeeName { get; set; }
        public string transectionDesc { get; set; }
        [Key]
        [Column(Order = 3)]
        public string DocType { get; set; }
        [Key]
        [Column(Order = 4)]
        public int DocID { get; set; }
        public int? CostCntrID { get; set; }
        public int? CostGrpID { get; set; }
    }
}