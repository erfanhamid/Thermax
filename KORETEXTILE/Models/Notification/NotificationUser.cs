using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Notification
{
    [Table("NotificationUser")]
    public class NotificationUser
    {
        public NotificationUser()
        {

        }

        public NotificationUser(string userId, string screenName)
        {
            UserId = userId;
            ScreenName = screenName;
        }

        [Key]
        [Column(Order =0)]
        public string UserId { set; get; }
        [Key]
        [Column(Order = 1)]
        public string ScreenName { set; get; }
    }
}