using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.AccountModule
{
    [Table("RequisitionMoneyVoucher")]
    public class RequisitionMoneyVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RSLNO { get; set; }
        public int EmpID { get; set; }
        public DateTime Date { get; set; }
        public decimal PrevBal { get; set; }
        public decimal TotalAmount { get; set; }
        public int CostCenterID { get; set; }
        public int IDP { get; set; }
        public int YearPart { get; set; }
        [NotMapped]
        public string AccountName { get; set; }
        [NotMapped]
        public int AccountId { get; set; }
        [NotMapped]
        public decimal Amount { get; set; }
        [NotMapped]
        public string Description { get; set; }
        [NotMapped]
        public string EmployeeName { get; set; }
        [NotMapped]
        public string EmployeeDesignation { get; set; }
        [NotMapped]
        public bool NegativeBalanceCheckbox { get; set; }
        [NotMapped]
        public int MRVNo { get; set; }
    }
}