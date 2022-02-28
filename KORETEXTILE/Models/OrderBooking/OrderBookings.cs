using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.OrderBooking
{
    [Table("OrderBooking")]
    public class OrderBookings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderNo { set; get; }    
        public DateTime OrderDate { set; get; } 
        public decimal InvoiceAmount { set; get; }
        public decimal CommissionAmt { set; get; }
        public decimal DiscountAmt { set; get; }
        public int SalesManID { set; get; }
        public int CustomerID { set; get; }
        public int CustBal { set; get; }
        public double clmCOGSTotal { set; get; }
        public decimal DisAmount { set; get; }
        public double VatAmount { set; get; }
        public string SaleType { set; get; }
        public string Description { get; set; }
        public string RefNo { get; set; }
    }
}