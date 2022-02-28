using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.OrderBooking
{
    [Table("OrderBookingLineItem")]
    public class OrderBookingLineItem
    {
        [Key]
        public int Id { set; get; }
        public int OrderNo { set; get; }    
        public int ItemID { set; get; }
        public int Qty { set; get; }
        public double Price { set; get; }
        public double SalesValue { set; get; }
        public int StoreID { set; get; }
        public double OfferQty { set; get; }
        public double clmCOGSRate { set; get; }
        public double clmCOGSValue { set; get; }
        [NotMapped]
        public string ItemName { set; get; }
        public double DisPerc { set; get; }
        [NotMapped]
        public decimal DisAmount { set; get; }
        public double VatPerc { set; get; }
        [NotMapped]
        public double VatAmount { set; get; }
        [NotMapped]
        public double CartonCapacity { set; get; }
        [NotMapped]
        public int GroupId { set; get; }
        [NotMapped]
        public string PacSize { set; get; }
    }
}