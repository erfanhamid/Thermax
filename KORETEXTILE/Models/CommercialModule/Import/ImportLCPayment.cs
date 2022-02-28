using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommercialModule.Import
{
    [Table("ImportLCPaymentD")]
    public class ImportLCPayment
    {
        [Key][Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ILCPNo { get; set; }
        [Required]
        public int ILCID { get; set; }
        public int AltILCNo { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int DebitAccID { get; set; }
        [Required]
        public int CreditAccID { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        public int SupplierId { set; get; }
    }
}