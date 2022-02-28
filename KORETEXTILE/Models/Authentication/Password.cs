using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.Authentication
{
    [Table("passwords")]
    public class Password
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string uname { get; set; }
        public int ulevel { get; set; }
        public string pword { get; set; }
        public DateTime firstuse { get; set; }
        public DateTime lastmod { get; set; }
        public DateTime edate { get; set; }
        public string UserType { get; set; }
        public string departmentsss { get; set; }
        public int brid { get; set; }
        public int empid { get; set; }
    }
}