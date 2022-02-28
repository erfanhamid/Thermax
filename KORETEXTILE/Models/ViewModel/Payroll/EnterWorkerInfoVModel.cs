using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class EnterWorkerInfoVModel
    {
        [Display(Name = "Worker ID")]
        public int WorkerID { get; set; }
        [Display(Name = "Worker Name")]
        public int WorkerName { get; set; }
        [Display(Name = "Designation")]
        public int DesignationID { get; set; }
        [Display(Name = "Type")]
        public int WorkerTypeID { get; set; }
        [Display(Name = "Card No")]
        public int CardNo { get; set; }
        [Display(Name = "Father's Name")]
        public int FatherName { get; set; }
        [Display(Name = "Mother's Name")]
        public int MotherName { get; set; }
        [Display(Name = "National ID")]
        public int NationalID { get; set; }
        [Display(Name = "Gender")]
        public int GenderID { get; set; }
        [Display(Name = "Date Of Birth")]
        public int DateOfBirth { get; set; }
        [Display(Name = "Religion")]
        public int ReligionID { get; set; }
        [Display(Name = "Present Address")]
        public int PresentAddress { get; set; }
        [Display(Name = "Permanent Address")]
        public int PermanentAddress { get; set; }
        [Display(Name = "Mobile No")]
        public int MobileNo { get; set; }
        [Display(Name = "Alternative No")]
        public int AltNo { get; set; }
        [Display(Name = "Work Station")]
        public int WorkStationID { get; set; }
        [Display(Name = "Section")]
        public int SectionID { get; set; }
        [Display(Name = "Joining Date")]
        public int JoiningDate { get; set; }

    }
}