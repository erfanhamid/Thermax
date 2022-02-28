using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.ProductionModule
{
    [Table("Batch")]
    public class Batch
    {
        [Key]
        [Display(Name ="Batch ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { set; get;}
        [Display(Name = "Batch Name")]
        public string BatchNo { set; get;}
        [Required]
        [Display(Name ="Batch Date")]
        public DateTime BatchDate { set; get;}
        [Display(Name ="Status")]
        public string Status { set; get;}
        public string BatchDesc { get; set; }
        public string PMName { get; set; }
        [NotMapped]
        public int GroupId { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
        [NotMapped]
        public string Date { get; set; }
    }
}