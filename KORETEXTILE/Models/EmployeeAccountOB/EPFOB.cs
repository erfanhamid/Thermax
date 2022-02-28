using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.EmployeeAccountOB
{
    [Table("EPFOB")]
    public class EPFOB
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime Tdate { get; set; }
        public string TRefNo { get; set; }
        public string TDescription { get; set; }
        [Required]
        public decimal TAmount { get; set; }
    }
}