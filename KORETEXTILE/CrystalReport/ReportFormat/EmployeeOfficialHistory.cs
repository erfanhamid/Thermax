using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class EmployeeOfficialHistory
    {
        public int EmployeeId { set; get; }
       
        public string Name { set; get; }
        
        public string OldDesignation { set; get; }
      
        public string NewDesignation { set; get; }
      
        public string OldDepatment { set; get; }
  
        public string NewDepatment { set; get; }
   
        public string OldFunctDesignation { set; get; }
        
        public string NewFunctDesignation { set; get; }
        
        public string OldSection { set; get; }
        
        public string NewSection { set; get; }
        
        public string OldBranch { set; get; }
        
        public string NewBranch { set; get; }
        
        public string OldCompany { set; get; }
        
        public string NewCompany { set; get; }
        
        public decimal OldGrossSalary { set; get; }
        
        public decimal NewGrossSalary { set; get; }
        
        public DateTime ApplicableDate { get; set; }
        public DateTime PreviousDate { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }
}