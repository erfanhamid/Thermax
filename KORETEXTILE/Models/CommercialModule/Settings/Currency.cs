using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.CommercialModule
{
    [Table("tblCurrency")]
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("clmCurrencyID")]
        public int CurrencyId { get; set; }
        [StringLength(50)]
        [Required]
        [Display(Name = "Currency Name")]
        [Column("clmCurrencyName")]
        public string CurrencyName { get; set; }
        [Required]
        [Display(Name = "Exchange Rate")]
        [Column("clmExchangeRate")]
        public decimal ExchangeRate { get; set; }
    }
}