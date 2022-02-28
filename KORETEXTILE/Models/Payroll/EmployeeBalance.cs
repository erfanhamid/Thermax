using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("EmployeeBalanceCalculation")]
    public class EmployeeBalance
    {
        public EmployeeBalance()
        {
        }

        public EmployeeBalance(int employeeID, DateTime date, string documentType, int accountID, string tRDescription, string refNo, decimal amount, int docNO)
        {
            EmployeeID = employeeID;
            TDate = date;
            DocumentType = documentType;
            AccountID = accountID;
            TRDescription = tRDescription;
            RefNo = refNo;
            Amount = amount;
            DocNO = docNO;
        }

        [Key]
        [Column(Order = 0)]
        public int EmployeeID { set; get; }
        public DateTime TDate { set; get; }
        public string DocumentType { set; get; }
        [Key]
        [Column(Order = 1)]
        public int AccountID { set; get; }
        public string TRDescription { set; get; }
        public string RefNo { set; get; }
        public decimal Amount { set; get; }
        [Key]
        [Column(Order = 2)]
        public int DocNO { set; get; }
    }
}