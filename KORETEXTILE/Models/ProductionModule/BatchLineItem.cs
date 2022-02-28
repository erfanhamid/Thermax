using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ProductionModule
{
    [Table("BatchLineItem")]
    public class BatchLineItem
    {
        [Key]
        [Column(Order =0)]
        public string BatchNo { set; get; }
        [Key]
        [Column(Order = 1)]
        public int ItemID { set; get; }
        public double Qty { set; get; }
        [Display(Name = "Expire Date")]
        public DateTime? ExpireDate { set; get; }
        [Display(Name = "Description")]
        public string BatchDesc { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public string BatchDate { set; get; }
        [NotMapped]
        public int groupId { get; set; }
        [NotMapped]
        public string PacSize { get; set; }
    }
}