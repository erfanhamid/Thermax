using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.SalesModule
{
    [Table("InstallmentBalanceCalculation")]
    public class InstallmentBalanceCalculation
    {
        public InstallmentBalanceCalculation()
        {

        }
        public InstallmentBalanceCalculation(int customerID, DateTime date, string documentType, int accountID, string trDesc, string refNo, decimal amount, int docNo)
        {
            RetailerID = customerID;
            TDate = date;
            DocumentType = documentType;
            AccountID = accountID;
            TrDesc = trDesc;
            RefNo = refNo;
            Amount = amount;
            DocNo = docNo;
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RetailerID { get; set; }
        public DateTime TDate { get; set; }
        [Key]
        [Column(Order = 1)]
        public string DocumentType { get; set; }
        public int AccountID { get; set; }
        public string TrDesc { get; set; }
        public string RefNo { get; set; }
        public decimal Amount { get; set; }
        [Key]
        [Column(Order = 2)]
        public int DocNo { get; set; }
    }
}