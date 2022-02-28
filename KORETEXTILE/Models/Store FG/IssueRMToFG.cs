using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Store_FG
{
    [Table("IssueRMToFG")]
    public class IssueRMToFG
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IRTFNo { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int IssueFrom { get; set; }
        [Required]
        public int IssueTo { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        public double TotalRMQty { get; set; }
        public double TotalRMStanderdCost { get; set; }
        public double TotalFGQty { get; set; }
        public double TotalFGStanderdCost { get; set; } 
    }
}