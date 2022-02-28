using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.SalesModule
{
    [Table("CustomerBalanceCalculation")]
    public class CustomerBalanceCalculation
    {
        public CustomerBalanceCalculation()
        {

        }
        public CustomerBalanceCalculation(int customerID, DateTime date, string documentType, int accountID, string trDesc, string refNo, decimal amount, int docNo)
        {
            CustomerID = customerID;
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
        public int CustomerID { get; set; }
        public DateTime TDate { get; set; }
        [Required]
        [Column(Order = 1)]
        public string DocumentType { get; set; }
        [Key]
        [Column(Order = 2)]
        public int AccountID { get; set; }
        public string TrDesc { get; set; }
        public string RefNo { get; set; }
        [Key]
        [Column(Order = 3)]
        public decimal Amount { get; set; }
        [Key]
        [Column(Order = 4)]
        public int DocNo { get; set; }

    }
}