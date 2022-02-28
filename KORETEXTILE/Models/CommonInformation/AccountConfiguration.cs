using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommonInformation
{
    [Table("AccountConfiguration")]
    public class AccountConfiguration
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public Int16 RefValue { set; get; }
    }
}