using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales.Transaction
{
    public class SalesVModel
    {
        public int InvoiceNo { set; get; }
        public DateTime SalesDate { set; get; }
        public double InvoiceAmount { set; get; }
        [DisplayName("Amount")]
        public decimal CommissionAmt { set; get; }
        public decimal DiscountAmt { set; get; }
        public int SalesManID { set; get; }
        public int CustomerID { set; get; }
        public int CustBal { set; get; }
        public double clmCOGSTotal { set; get; }
        public int IDP { set; get; }
        public int YearPart { set; get; }
        public int ItemID { set; get; }
        public int Qty { set; get; }
        public double Price { set; get; }
        public double SalesValue { set; get; }
        public int StoreId { set; get; }
        public double OfferQty { set; get; }
        public double clmCOGSRate { set; get; } 
        public double clmCOGSValue { set; get; }
        public int SRId { set; get; }
        public int DepotId { set; get; }
        public string SrName { set; get; }
        public string SrDesignation { set; get; }   
        public string Area { set; get; }
        public int IsRealSale{set;get;}
        public int GroupId { set; get; }
        public int Item { set; get; }
        public double PcsPerCtn { set; get; }
        public double SoldQuantity { set; get; }
        public double PricePerPcs { set; get; }
        public double Balance { set; get; }
        public double InvoiceTotal { set; get; }
        public decimal Comission { set; get; }
        public double Amount1 { set; get; }
        public decimal NetAmount { set; get; }
        public decimal Discount { set; get; }
        public double AfterDisciount { set; get; }
        public decimal NetInvoice { set; get; }
        public string ZoneId { get; set; }
        public double FreeQty { get; set; }
        public double TotalQty { get; set; }
        public double CreditLimit { set; get; }
        public double TotalAmount { set; get; }
        public string CustName { set; get; }
        [DisplayName("Vat (%)")]
        public double VatPerc { set; get; }
        [DisplayName("Vat Amount")]
        public double VatAmount { set; get; }
        [DisplayName("After Vat")]
        public double AfterVat { set; get; }
        [DisplayName("Discount (%)")]
        public double DisPerc { set; get; }
        [DisplayName("Discount Amount")]
        public double DisAmount { set; get; }
        [DisplayName("After Discount")]
        public double AfterDis { set; get; }
        [DisplayName("Total vat")]
        public double TotVat { set; get; }
        [DisplayName("Tptal Discount")]
        public double TotDiscount { set; get; }
        public int CreditDays { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string Type { get; set; }
        public string PacSize { set; get; }
        public double Carton { set; get; }
        public double AvalCarton { set; get; }
        public string SaleType { set; get; }
        public string Remarks { get; set; }
        public string  Description { get; set; }
        public string CustMobileNo { get; set; }
        [DisplayName("Ref No")]
        public string RefNo { get; set; }
    }
}