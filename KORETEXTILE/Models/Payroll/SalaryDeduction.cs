using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    public class SalaryDeduction
    {
        [Key][Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }
        [Required][Display(Name ="Advance Adjust")]
        public double AdvanceAdjust { get; set; }
        [Required]
        [Display(Name = "Tax")]
        public double Tax { get; set; }
        [Required]
        [Display(Name = "T/M Phone")]
        public double TMPhone { get; set; }
        [Required]
        [Display(Name = "PF (OWN)")]
        public double PFOwn { get; set; }
        [Required]
        public double MessBill { get; set; }
        [Required]
        public double HouseRent { get; set; }
        [Required]
        public double UtilityBill { get; set; }
        [Required]
        public double CanteenBill { get; set; }
        [Required]
        public double SecurityReceive { get; set; }
        [Required]
        public double RevenuStamp { get; set; }
        [Required]
        public double OtherAdjust { get; set; }
        [Required]
        public double FSales { get; set; }
        [Required]
        public double Loan { get; set; }
        [Required]
        public double TDS { get; set; }
    }
}