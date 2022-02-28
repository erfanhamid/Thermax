using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class EmpInfoReport
    {
        public int EmployeeNo { set; get; }
        public string Name { set; get; }
        public string EmpGrade { set; get; }
        public string FatherName { set; get; }
        public string MotherName { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string Gender { set; get; }
        public string ReligionName { set; get; }
        public string MaritalStatus { set; get; }
        public string BloodGroup { set; get; }
        public string Qualifications { set; get; }
        public string NomineeName { set; get; }
        public string NomineeRelation { set; get; }
        public string FuncDesignation { set; get; }
        public int Section { set; get; }
        public string BrnachName { set; get; }
        public string Desingnation { set; get; }
        public string Department { set; get; }
        public DateTime JoiningDate { set; get; }
        public double Salary { set; get; }
        public string Email { set; get; }
        public string PersonalEmail { set; get; }
        public string NationalID { set; get; }
        public string PresentAddress { set; get; }
        public string PermanentAddress { set; get; }
        public string Mobile { set; get; }
        public string AlternativeContct { set; get; }

    }
}