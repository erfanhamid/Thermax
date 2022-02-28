using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Notification
{
    [Table("ApprovalLog")]
    public class ApprovalLog
    {
        [Key]
        [Column(Order =0)]
        public int NotificationId { set; get; }
        [Key]
        [Column(Order = 1)]
        public string UserId { set; get; }
        public DateTime NotifiTime { set; get; }
        public DateTime ApprovalTime { set; get; }
        public bool IsView { set; get; }
        public bool IsApproved { set; get; }
    }
}