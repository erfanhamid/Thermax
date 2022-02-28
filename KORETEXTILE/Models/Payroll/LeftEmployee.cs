using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("LeftEmployee")]
    public class LeftEmployee
    {
        [Required]
        [Display(Name = "Employee No")]

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { set; get; }
        [Required]
        [Display(Name = "Employee Name")]
        public string Name { set; get; }
        [Display(Name = "Employee Grade")]
        public string EmpGrade { set; get; }
        [Required]
        [Display(Name = "EOB")]
        public double OpBalance { set; get; }
        [Display(Name = "As On")]
        public DateTime OBDate { set; get; }
        [Display(Name = "Father's Name")]
        public string FatherName { set; get; }
        [Display(Name = "Mother's Name")]
        public string MotherName { set; get; }
        [Display(Name = "Nominee Name")]
        public string NomineeName { set; get; }
        [Display(Name = "Relation With Nominee")]
        public string NomineeRelation { set; get; }
        [Display(Name = "Present Address")]
        public string PresentAddress { set; get; }
        [Display(Name = "Permanent Address")]
        public string PermanentAddress { set; get; }
        [Display(Name = "Mobile No")]
        public string Mobile { set; get; }
        [Display(Name = "Office Email")]
        public string Email { set; get; }
        [Required]
        [Display(Name = "Gender")]
        public int Sex { set; get; }
        [Display(Name = "DOB")]
        public DateTime DateOfBirth { set; get; }
        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { set; get; }
        [Display(Name = "Confirmation Date")]
        public DateTime? ConfirmationDate { get; set; }
        [Required]
        public int Designation { set; get; }
        [Display(Name = "Highest Education")]
        public int HighestEducation { set; get; }
        public double Salary { set; get; }
        [Display(Name = "Marital Status")]
        public int MarstausID { set; get; }
        [Display(Name = "Religion")]
        public int ReligionID { set; get; }
        [Display(Name = "Department")]
        public int DepatmentID { set; get; }
        [Display(Name = "Blood Group")]
        public string BloodGroup { set; get; }
        [Display(Name = "Alternative No")]
        public string AlternativeContct { set; get; }
        [Display(Name = "Personal Email")]
        public string PersonalEmail { set; get; }
        [Display(Name = "National Id")]
        public string NationalID { set; get; }
        [Display(Name = "Functional Designation")]
        public int FunctDesignation { set; get; }
        public int Section { set; get; }
        [Display(Name = "Work Station")]
        public int BranchID { set; get; }
        [Display(Name = "Staff Type")]
        public string StaffType { set; get; }
        public string LogInUserName { set; get; }
        [Display(Name = "Company")]
        public int CompanyId { set; get; }
        [Display(Name = "Bank A/C")]
        public string BankAccount { get; set; }
        [NotMapped]
        public string CompanyName { get; set; }
        [NotMapped]
        public string DesignationName { get; set; }
        [NotMapped]
        public string FD { get; set; }
        [NotMapped]
        public string WorkStation { get; set; }
        [NotMapped]
        public string Department { get; set; }
        [NotMapped]
        public string SectionName { get; set; }
        [NotMapped]
        public string HigherEdu { get; set; }
        [NotMapped]
        public string ReligionName { get; set; }
        [NotMapped]
        public string GenderName { get; set; }
        [NotMapped]
        public string MaritalStatus { get; set; }
    }
}