using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Authentication
{
    [Table("UserWiseApproved")]
    public class UserWiseApproved
    {
        public int Id { set; get; }
        public string UserId { set; get; }
        public string Type { set; get; }
    }
}