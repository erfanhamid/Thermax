using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.CommercialModule.Import
{
    [Table("tblILCB")]
    public class ILCB
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ILCBNo { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Column("LCID")]
        public int ILCID { get; set; }  
        [Required]
        public int SupplierID { get; set; }
        public decimal BillTotalValue { get; set; }
        //[Required]
        //public string Type { get; set; }
      
    }
}