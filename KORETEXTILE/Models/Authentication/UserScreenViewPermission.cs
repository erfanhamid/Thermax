using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LOgTest.Models.Authentication
{
    public class UserScreenViewPermission
    {
        [Key]
        [Column(Order =0)]
        public string ScreenName { set; get; }
        [Key]
        [Column(Order = 1)]
        public string RoleId { set; get; }
    }
}