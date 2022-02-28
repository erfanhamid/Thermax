using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.AccountModule
{
    [Table("EPVLineItem")]
    public class EPVLineItem
    {
        [Key,Column(Order =0)]
        public int EPVNo { get; set; }
        [Key, Column(Order = 1)]
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string WorkStation { get; set; }
        [NotMapped]
        public string OrganicDesignation { get; set; }
        [NotMapped]
        public string FunctionalDesignation { get; set; }
    }
}