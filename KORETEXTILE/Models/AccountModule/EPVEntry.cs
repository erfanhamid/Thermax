using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.AccountModule
{
    [Table("EPVEntry")]
    public class EPVEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EPVNo { get; set; }
        public int  PayAccId{ get;set;}
        public DateTime Date { get; set;}
        public string RefNo { get; set; }
        public string Description { get; set; }
        public decimal EPVTotal { get; set; }
    }
}