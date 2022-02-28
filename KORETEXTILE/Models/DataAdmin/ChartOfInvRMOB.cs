using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Data_Admin
{
    [Table("RMOB")]
    public class ChartOfInvRMOB
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Column("RMOBNO")]
        public int RMOBNO { set; get; }
        [Column("StoreID")]
        public int StoreID { set; get; }
        [Column("DepotID")]
        public int DepotID { set; get; }
    }
}