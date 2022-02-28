using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Authentication
{
    public class UserWiseModule
    {
        [Key]
        [Column(Order = 0)]
        public string ModuleName { set; get; }
        [Key]
        [Column(Order = 1)]
        public string UserName { set; get; }
    }
}