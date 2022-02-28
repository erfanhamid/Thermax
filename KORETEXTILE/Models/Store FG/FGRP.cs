using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Store_FG
{
    [Table("tblFGRP")]
    public class FGRP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("clmFGRPNo")]
        public int FGRPNo { set; get; }
        [Column("clmFGRPDate")]
        [Required]
        public DateTime FGRPDate { set; get; }
        [Column("clmIFGFNo")]
        [Required]
        public int IFGFNo { set; get; }
        [Required]
        [Column("clmIFGSDate")]
        public DateTime IFGSDate { set; get; }
        [Column("clmTotalQT")]
        public decimal TotalQT { set; get; }
        [Column("clmTotalCost")]
        public decimal TotalCost { set; get; }
        public int IDP { get; set; }
        public int YearPart { get; set; }
    }
}