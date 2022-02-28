using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Data_Admin
{
    [Table("tblFGOB")]
    public class ChartOfInvFGOB
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Column("clmFGOBNO")]
        public int FGOBNO { set; get; }
        [Column("clmStoreID")]
        public int StoreID { set; get; }
         [Column("clmDepotID")]
        public int DepotID { set; get; }
    }
}