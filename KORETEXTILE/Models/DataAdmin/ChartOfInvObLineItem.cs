using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Data_Admin
{
    [Table("tblRMOBLine")]
    public class ChartOfInvObLineItem
    {
        [Key]
        [Column("clmRMOBNO", Order = 0)]
        public int RMOBNO { set; get; }
        [Column("tblItemGrpID")]
        public int ItemGrpID { set; get; }
        [Key]
        [Column("tblItemID", Order = 1)]
        public int ItemID { set; get; }
        [Column("clmOBQT")]
        public int OBQT { set; get; }
        [Column("UnitID")]
        public int UnitID { set; get; }
        [Column("clmSTCost")]
        public decimal STCost { set; get; }
        [Column("clmValue")]
        public decimal Value { set; get; }


    }
}