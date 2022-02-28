using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("CheckInAttandanceLog")]
    public class CheckInAttandanceLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public DateTime CheckIn { get; set; }
        public bool IsCalculate { set; get; }
        [NotMapped]
        public DateTime CheckOut { get; set; }
        [NotMapped]
        public DateTime Date { get; set; }
        //[NotMapped]
        //public HttpPostedFileBase ExcelFile { get; set; }
    }
}