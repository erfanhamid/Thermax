using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.SMS
{
    [Table("SMSSuccess")]
    public class SMSSuccess
    {
        public int Id { set; get; }
        public string Sender { set; get; }
        public string Receiver { set; get; }
        public string Message { set; get; }
        public DateTime SendingTime { set; get; }
    }
}