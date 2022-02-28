using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Procurement
{
    [Table("tblGeneralBillslineitem")]
    public class GeneralBillsLineItem
    {
        [Key, Column("clmGBENo",Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Column("clmGBENo")]
        [Display(Name = "GBE No")]
        public int GBENo { get; set; }
        [Key, Column("clmDebitAccID",Order = 1)]
        //[Column("clmDebitAccID")]
        [Display(Name = "COA")]
        public int DebitAccId { get; set; }
        [NotMapped]
        public string DebitAccountName { get; set; }
        [Display(Name = "Cost Group")]
        [Column("clmCGID")]
        public int CGId { get; set; }
        [NotMapped]
        public string CostGroupName { get; set; }
        [Column("clmAmount")]
        public decimal Amount { get; set; }
        [Display(Name = "Description")]
        [Column("clmDescriptions")]
        public string Descriptions { get; set; }

    }
}