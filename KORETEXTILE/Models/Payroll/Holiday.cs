using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace BEEERP.Models.Payroll
{
    [Table("Holiday")]
    public class Holiday
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int  ID { get; set; }
        [Required]
        [Display(Name ="Holiday Name")]
        public string HolidayName { get; set; }
        [Required]
        [Display(Name ="Holiday Date")]
        public DateTime HolidayDate { get; set; }
        
        [NotMapped]
        public string Date { get; set; }
      

    }
}