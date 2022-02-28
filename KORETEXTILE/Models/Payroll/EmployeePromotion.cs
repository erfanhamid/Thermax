using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("EmployeePromotion")]
    public class EmployeePromotion
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PromotionID { get; set; }
        public int EmployeeID { get; set; }
        
        [Required]
        public DateTime EffectiveDate { get; set; }
        public int OldDesignationID { get; set; }
        public int NewDesignationID { get; set; }
        public int OldFuncDesignationID { get; set; }
        public int NewFuncDesignationID { get; set; }
        public int OldGrade { get; set; }
        public int NewGrade { get; set; }
        public string PRefNO { get; set; }
        public string PDescription { get; set; }
        [NotMapped]
        public string OldGradeName { get; set; }
        [NotMapped]
        public string OldDesignationName { get; set; }
        [NotMapped]
        public string OldFuncDesignationName { get; set; }

    }
}