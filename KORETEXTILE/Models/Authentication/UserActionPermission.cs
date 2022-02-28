using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LOgTest.Models.Authentication
{
    [Table("UserActionPermission")]
    public class UserActionPermission
    {
        [Key]
        [Column(Order = 0)]
        public string UserId { set; get; }
        [Key]
        [Column(Order = 1)]
        public string Action { set; get; }
        [Key]
        [Column(Order = 2)]
        public string ScreenName { set; get; }
        [Key]
        [Column(Order = 3)]
        public string Type { set; get; }
    }
}