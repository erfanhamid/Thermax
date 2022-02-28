using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommercialModule
{
    [Table("tblLTRBalCalculation")]
    public class LTRBalCalculations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }
        [Column("clmLTRID")]
        public int LTRID { get; set; }
        [Column("clmDate")]
        public DateTime Date { get; set; }
        [Column("clmPaymentAccountID")]
        public int ACCID { get; set; }
        [Column("clmAmount")]
        public decimal Amount { get; set; }
        [Column("clmDescriptions")]
        public string Description { get; set; }
        [Column("clmRefno")]
        public string RefNo { get; set; }
        [Column("clmDocType")]
        public string DocType { get; set; }
        [Column("clmDocNo")]
        public int DocNo { get; set; }
    }
}