using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("WorkerInformation")]
    public class EnterWorkerInformation
    {
        //public int Id { set; get; }
        //[Required]
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkerID { get; set; }
        [Required]
        public string WorkerName { get; set; }
        [Required]
        public int DesignationID { get; set; }
        [Required]
        public int WorkerTypeID { get; set; }
        public int CardNo { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string NationalID { get; set; }
        [Required]
        public int GenderID { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public int ReligionID { get; set; }
        [Required]
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        [Required]
        public string MobileNo { get; set; }
        public string AltNo { get; set; }
        [Required]
        public int WorkStationID { get; set; }
        [Required]
        public int SectionID { get; set; }
        [Required]
        public DateTime JoiningDate { get; set; }
    }
}