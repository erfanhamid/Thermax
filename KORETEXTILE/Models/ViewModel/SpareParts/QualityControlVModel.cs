using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.ViewModel.SpareParts
{
    public class QualityControlVModel
    {
        public int QcNo { get; set; }
        public DateTime QcDate { get; set; }
        public int LCRNNo { get; set; }
        public int CompanyID { get; set; }
        public string ReferenceNo { get; set; }
        public string Description { get; set; }
        public int ItemID { get; set; }
        public decimal Qty { get; set; }
        public decimal QcQty { get; set; }
        public decimal CostRt { get; set; }
        public decimal CostVal { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public int GroupID { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
        [NotMapped]
        public string UoM { get; set; }
        [NotMapped]
        public int SupplierID { get; set; }
    }
}