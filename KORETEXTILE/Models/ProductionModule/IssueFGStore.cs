using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ProductionModule
{
    [Table("tblIFGS")]
    public class IssueFGStore
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clmIFGSID { get; set; }
        [Required]
        public DateTime clmIFGSDate { get; set; }
        [Required]
        public decimal clmIFGSTotal { get; set; }
        public decimal clmCostTotal { get; set; }
        public string clmDescription { get; set; }
        public string clmRefno { get; set; }
        public string clmGRQNO { get; set; }
        [NotMapped]
        public int IssueFrom { set; get; }
        [NotMapped]
        public int IssueTo { set; get; }
    }
}