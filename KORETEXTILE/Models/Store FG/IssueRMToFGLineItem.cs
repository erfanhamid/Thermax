using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Store_FG
{
    [Table("IssueRMToFGLineItem")]
    public class IssueRMToFGLineItem
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IRTFNo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int RMItemId { get; set; }
        public double RMQty { get; set; }
        public double RMStanderdCost { get; set; }
        public double RMTotal { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int FGItemId { get; set; }
        public double FGQty { get; set; }
        public double FGStanderdCost { get; set; }
        public double FGTotal { get; set; } 
        [NotMapped]
        public string RMItemName { get; set; }
        [NotMapped]
        public string PacSizeIssue { get; set; }
        [NotMapped]
        public string FGItemName { get; set; }
        [NotMapped]
        public string PacSizeTo { get; set; }
    }
}