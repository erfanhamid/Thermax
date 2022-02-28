using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.AccountModule
{
    [Table("MoneyRequisitionVoucherAdjust")]
    public class MoneyRequisitionVoucherAdjust
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MRVANo { get; set; }
        public DateTime Date { get; set; }
        public int CostCenterId { get; set; }
        public int EmpID { get; set; }
        public double PreviousBalance { get; set; }
        public double VoucherTotal { get; set; }
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