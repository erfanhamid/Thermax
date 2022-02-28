using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("EmployeeTransfer")]
    public class EmployeeTransfer
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TransferID { get; set; }
        public int EmployeeID { get; set; }
        [Required]
        public DateTime EffectiveDate { get; set; }
        public int PreviousBranchID { get; set; }
        public int PresentBranchID { get; set; }
        
        public string TRefNO { get; set; }
        public string TDescription { get; set; }
        [NotMapped]
        public string PreviousBranch { get; set; }
        [NotMapped]
        public string PresentBranch { get; set; }
    }
}