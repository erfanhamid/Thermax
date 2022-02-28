using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Data_Admin
{
    [Table("tblRMOB")]
    public class ChartOfInvOb
    {
        [Key]
        [Column("clmRMOBNO")]
        public int RMOBNO { set; get; }
        [Column("clmStoreID")]
        public int StoreID { set; get; }
        [Column("clmDepotID")]
        public int DepotID { set; get; }
    }
}