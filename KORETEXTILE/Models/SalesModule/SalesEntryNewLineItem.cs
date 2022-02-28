using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.SalesModule
{
    [Table("SalesEntryNewLineItem")]
    public class SalesEntryNewLineItem
    {
        [Key]
        public int Id { set; get; }
        public int InvoiceNo { set; get; }
        public int ItemID { set; get; }
        public double Qty { set; get; }
        public decimal Price { set; get; }
        public decimal SalesValue { set; get; }
        public int StoreID { set; get; }
        public double OfferQty { set; get; }
        public decimal clmCOGSRate { set; get; }
        public decimal clmCOGSValue { set; get; }
        [NotMapped]
        public decimal COGOValue { get; set; }
        [NotMapped]
        public string ItemName { set; get; }
        [NotMapped]
        public double DisPerc { set; get; }
        [NotMapped]
        public decimal DisAmount { set; get; }
        [NotMapped]
        public double VatPerc { set; get; }
        [NotMapped]
        public decimal VatAmount { set; get; }
        [NotMapped]
        public double CartonCapacity { set; get; }
        [NotMapped]
        public int GroupId { set; get; }
        [NotMapped]
        public string PacSize { set; get; }
        [NotMapped]
        public string StoreName { get; set; }
    }
}