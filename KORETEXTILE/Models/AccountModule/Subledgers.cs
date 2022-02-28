using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.AccountModule
{
    [Table("tblSubledgers")]
    public class Subledgers
    {
        [Key,Column("clmSubLedgerID",Order =0)]
        [Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
       // [Column("clmSubLedgerID")]
        public int SubLedgerID { get; set; }
        [Required]
        [Column("clmSubLdgerName")]
        public string SubLdgerName { get; set; }
        [Required]
        [Key,Column("clmGLID",Order =1)]
        public int GLID { get; set; }
        [Required]
        [Column("clmType")]
        public string Type { get; set; }  
       [NotMapped]
        public string GLName { get; set; }
    }
}