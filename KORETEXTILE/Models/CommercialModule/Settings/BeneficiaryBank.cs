using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.CommercialModule
{
    [Table("tblBeneficiaryBank")]
    public class BeneficiaryBank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("clmBankNo")]
        public int BankId { get; set; }
        [Required]
        [StringLength(50)]
        [Column("clmBankName")]
        public string BankName { set; get; }
    }
}