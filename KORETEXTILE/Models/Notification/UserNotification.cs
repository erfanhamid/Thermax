using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Notification
{
    [Table("Notification")]
    public class UserNotification
    {
        
        public int Id { set; get; }
        public string NotificationHead { set; get; }
        public string NotificationDetails { set; get; }
        public DateTime PostingDate { set; get; }
        public string UserId { set; get; }
        public string Type { set; get; }
        public string TransactionNo { set; get; }
        public int BranchId { set; get; }
    }
}