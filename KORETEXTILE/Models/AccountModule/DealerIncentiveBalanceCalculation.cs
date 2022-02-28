using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.AccountModule
{

    [Table("DealerIncentiveBalanceCalculation")]
    public class DealerIncentiveBalanceCalculation
    {

        public DealerIncentiveBalanceCalculation()
        {

        }
        public DealerIncentiveBalanceCalculation(int dealerID, DateTime date, string docType, int accountID, string description, string refNo, decimal amount, int docNo)
        {
            DealerID = dealerID;
            Date = date;
            DocType = docType;
            AccountID = accountID;
            Description = description;
            RefNo = refNo;
            Amount = amount;
            DocNo = docNo;
        }

        [Key, Column(Order = 0)]
        [Required]
        public int DealerID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        public string DocType { get; set; }
        [Required]
        public int AccountID { get; set; }
        public string Description { get; set; }
        public string RefNo { get; set; }
        public decimal Amount { get; set; }
        [Key, Column(Order = 2)]
        [Required]
        public int DocNo { get; set; }
    }
}