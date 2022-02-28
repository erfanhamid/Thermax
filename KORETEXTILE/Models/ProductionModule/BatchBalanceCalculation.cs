using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ProductionModule
{
    [Table("BatchBalanceCalculation")]
    public class BatchBalanceCalculation
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocNo { get; set; }
        [Key]
        [Column(Order = 1)]
        public int BatchId { get; set; }
        public int AccountId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string DocType { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }

        public BatchBalanceCalculation() { }
        public BatchBalanceCalculation(int docno, int batchid, int accountid, string doctype, DateTime date, decimal amount, string refno, string description)
        {
            DocNo = docno;
            BatchId = batchid;
            AccountId = accountid;
            DocType = doctype;
            Date = date;
            Amount = amount;
            RefNo = refno;
            Description = description;
        }
    }
}