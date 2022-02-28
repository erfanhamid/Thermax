using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Production
{
    public class BatchViewModel
    {
       
        
        public int ID { set; get; }
        public string BatchNo { set; get; }
        public DateTime BatchDate { set; get; }
        public string BatchDesc { get; set; }
        public string Status { set; get; }
        public int GroupId { get; set; }
        public int ItemID { set; get; }
        public double Qty { set; get; }
        public DateTime? ExpireDate { set; get; }
        public string UOM { get; set; }
    }
}