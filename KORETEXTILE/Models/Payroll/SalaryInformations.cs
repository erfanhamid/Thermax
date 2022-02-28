using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    public class SalaryInformations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public DateTime ProcessDate { get; set; }
        [Required]
        public double Basic { get; set; }
        [Required]
        public double HRent { get; set; }
        [Required]
        public double Conv { get; set; }
        [Required]
        public double Medical { get; set; }
        [Required]
        public double GrossSalary { get; set; }
        [Required]
        public int Week { get; set; }
        [Required]
        public int Pay { get; set; }
        [Required]
        public double EarnedSalary { get; set; }
        [Required]
        public decimal OvertimeHours { get; set; }
        [Required]
        public double OvertimeTaka { get; set; }
        [Required]
        public double Arrear { get; set; }
        [Required]
        public double TotalAddition { get; set; }
        [Required]
        public double AdvanceAdjust { get; set; }
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
        public double Tax { get; set; }
        [Required]
        public double TMPhone { get; set; }
        [Required]
        public double PFOwn { get; set; }
        [Required]
        public double RevenueStamp { get; set; }
        [Required]
        public double OtherAdjust { get; set; }
        [Required]
        public double FSales { get; set; }
        [Required]
        public double TotalDeduct { get; set; }
        [Required]
        public double PayableSalary { get; set; }
        [Required]
        public string PayMode { get; set; }
    }
}