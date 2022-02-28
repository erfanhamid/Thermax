using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommercialModule.Import
{
    [Table("tblLCAccBalCalculation")]
    public class ILCBalCalculations
    {
        [Key]
        public int Id { get; set; }
        [Column("clmILCID")]
        public int ILCID { get; set; }
        [Column("clmDate")]
        public DateTime Date { get; set; }
        [Column("clmPaymentAccountID")]
        public int PaymentAccountID { get; set; }
        [Column("clmAmount")]
        public decimal Amount { get; set; }
        [Column("clmDescriptions")]
        public string Descriptions { get; set; }
        [Column("clmRefno")]
        public string Refno { get; set; }
        [Column("clmDocType")]
        public string DocType { get; set; }
        [Column("clmDocNo")]
        public int DocNo { get; set; }
        public int ILCPNo { set; get; }
    }
}