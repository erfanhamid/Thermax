using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.SpareParts
{
    public class MRRGoodsReceiveNoteVModel
    {
        public int LCRNNo { get; set; }
        public DateTime Date { get; set; }
        public int QCNo { get; set; }
        public int? LCNo { get; set; }
        public int StoreId { get; set; }
        public int CompanyID { get; set; }
        public int TypeId { get; set; }
        public int? BoxID { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int ItemId { get; set; }
        public decimal QcQty { get; set; }
        public decimal? CostRt { get; set; }
        public decimal? CostVal { get; set; }
        public int LCId { get; set; }
        [NotMapped]
        public string StoreName { get; set; }
        [NotMapped]
        public string BoxName { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
    }
}