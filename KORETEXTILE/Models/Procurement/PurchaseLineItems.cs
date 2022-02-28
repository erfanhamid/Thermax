﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Procurement
{
    [Table("PurchaseLineItems")]
    public class PurchaseLineItems
    {
       
        [Required]
        public int BillNo { get; set; }
        [Column("GroupID")]
        public int GroupId { get; set; }
        [Required]
        [Key, Column("ItemID", Order = 0)]
        public int ItemId { get; set; }
        [Column("UoMID")]
        public int UOMId { get; set; }
        [Required]
        public decimal Qty { get; set; }
        [Required]
        [Column("Rate")]
        public decimal UnitCost { get; set; }
        [Required]
        [Column("Value")]
        public decimal TotalCost { get; set; }
        [Key, Column(Order = 1)]
        public int GRNNo { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public string Unit { get; set; }
        [NotMapped]
        [Column("clmVat")]
        public double Vat { get; set; }
        [NotMapped]
        [Column("clmAIT")]
        public double Ait { get; set; }
        [NotMapped]
        public double VDA { get; set; }
    }
}