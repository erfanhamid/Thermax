using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.GeneralStore
{
    [Table("GSInventoryIssue")]
    public class GSInventoryIssue
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GSIIssueNo { get; set; }
        [Required]
        public DateTime GSIssueDate { get; set; }
        [Required]
        public int StoreID { get; set; }
        [Required]
        public int CompanyID { get; set; }
        [Required]
        public int DepartmentID { get; set; }
        public string ReferenceNo { get; set; }
        public string GSIssueDescription { get; set; }
    }
}