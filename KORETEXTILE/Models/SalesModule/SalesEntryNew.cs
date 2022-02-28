using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.SalesModule
{
    [Table("SalesEntryNew")]
    
    public class SalesEntryNew
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InvoiceNo { set; get; }
        public DateTime SalesDate { set; get; }
        public decimal InvoiceAmount { set; get; }
        public decimal CommissionAmt { set; get; }
        public decimal DiscountAmt { set; get; }
        public int SalesManID { set; get; }
        public int CustomerID { set; get; }
        public int CustBal { set; get; }
        public decimal clmCOGSTotal { set; get; }
        [NotMapped]
        public string Remarks { get; set; }
        [NotMapped]
        public string Description { get; set; }       
        [NotMapped]
        public decimal DisAmount { set; get; }
        [NotMapped]
        public decimal VatAmount { set; get; }
        [NotMapped]
        public int OrderNumber { get; set; } 
        [NotMapped]
        public int BranchId { set; get; }
        [NotMapped]
        public string SaleType { set; get; }
        [NotMapped]
        public string RefNo { get; set; }
        

    }
}