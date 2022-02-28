using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Data_Admin
{
    [Table("tblFGOBLine")]
    public class ChartOfInvFGOBLineItem
    {
        [Key]
        [Column("clmFGOBNO",Order =0)]
        public int FGOBNO { set; get; }
        [Column("tblItemGrpID")]
        public int ItemGrpID { set; get; }
        [Key]
        [Column("tblItemID",Order =1)]
        public int ItemID { set; get; }
        [Column("clmOBQT")]
        public int OBQT { set; get; }
        [Column("clmUnitID")]
        public int UnitID { set; get; }
        [Column("clmSTCost")]
        public decimal STCost { set; get; }
        [Column("clmValue")]
        public decimal Value { set; get; }
        [Column("clmOBCarton")]
        public double OBCarton { set; get; }
        [Column("clmOBLitre")]
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