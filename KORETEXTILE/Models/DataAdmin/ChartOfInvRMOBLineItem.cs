using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Data_Admin
{
    [Table("RMOBLine")]
    public class ChartOfInvRMOBLineItem
    {
        [Key]
        [Column("RMOBNO", Order = 0)]
        public int RMOBNO { set; get; }
        [Column("ItemGrpID")]
        public int ItemGrpID { set; get; }
        [Key]
        [Column("ItemID", Order = 1)]
        public int ItemID { set; get; }
        [Column("OBQT")]
        public int OBQT { set; get; }
        [Column("UnitID")]
        public int UnitID { set; get; }
        [Column("STCost")]
        public decimal STCost { set; get; }
        [Column("Value")]
        public decimal Value { set; get; }
        [Column("OBCarton")]
        public double OBCarton { set; get; }
        [Column("OBLitre")]
        public double OBLitre { set; get; }
        [NotMapped]
        public string ItemName { set; get; }
        [NotMapped]
        public int GroupId { set; get; }
        [NotMapped]
        public string ItemCode { set; get; }
        [NotMapped]
        public string PacSize { get; set; }

    }
}