using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Data_Admin
{
    [Table("MoneyRequisitionOB")]
    public class MoneyRequisitionOB
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime MROBDate { get; set; }
        public decimal Amount { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
    }
}