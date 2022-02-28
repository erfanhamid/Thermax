using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.AccountModule
{
    [Table("tblMRVAReference")]
    public class MRVAReference
    {
        [Key, Column("clmMRVNo", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MRVAId { get; set; }
        [Key, Column("clmRefNo", Order = 1)]
        public int MRVId { get; set; }
    }
}