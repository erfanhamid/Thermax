using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.AccountModule
{
    [Table("SupplierAccAdjustment")]
    public class SupplierAcAdjustment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("SAAdjustmentNo")]
        public int SAADNo { get; set; }
        [Column("SAADate")]
        public DateTime Date { get; set; }
        [Column("LedgerACCID")]
        public int LedgerAcId { get; set; }
        [Column("SupplierID")]
        public int SupplierId { get; set; }
        [Column("SAAdjAmount")]
        public decimal Amount { get; set; }
        [Column("RefNo")]
        public string RefNo { get; set; }
        [Column("Note")]
        public string Description { get; set; }
        public int GroupID { get; set; }
        public string BillType { get; set; }
        public int BillNo { get; set; }
        [NotMapped]
        [HiddenInput(DisplayValue =false)]
        [Display(Name = "Balance")]
        public decimal AdBalance { get; set; }
        [NotMapped]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Balance")]
        public decimal EmpBalance { get; set; }
        [NotMapped]
        [Display(Name = "TotalPaidAmount")]
        public decimal TotalPaidAmount { get; set; }

    }
}