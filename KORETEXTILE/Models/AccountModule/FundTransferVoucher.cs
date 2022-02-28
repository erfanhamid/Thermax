using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.AccountModule
{
    [Table("FundTransfer")]
    public class FundTransferVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Transfer No")]
        [Required]
        [Column("TransferID")]
        public int TransferId { get; set; }
        [Column("TftDate")]
        public DateTime Date { get; set; }
        [Display(Name = "Transfer From")]
        [Required]
        [Column("TransferFrom")]
        public int TransFrom { get; set; }
        [Display(Name = "Transfer To")]
        [Required]
        [Column("TransferTo")]
        public int TransTo { get; set; }
        [Display(Name = "Transfer Amount")]
        [Required]
        [Column("TfrAmount")]
        public decimal TransAmount { get; set; }
        [Display(Name = "Reference No")]
        public string RefNo { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        public int IDP { get; set; }
        public int YearPart { get; set; }
    }
}