using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Log
{
    public class UserLog
    {
        public UserLog()
        {

        }
        public UserLog(string trnasId, DateTime transDate, string screenName, string action)
        {
            TrnasId = trnasId;
            TransDate = transDate;
            ScreenName = screenName;
            Action = action;
            Ip = System.Net.Dns.GetHostName() ;
            LogInName = HttpContext.Current.User.Identity.Name;
            UserActualDate = DateTime.Now;
        }

        [Key]
        [Column(Order =0)]
        public string TrnasId { private set; get; }
       
        public DateTime TransDate { set; get; }
        [Key]
        [Column(Order = 1)]
        public string ScreenName { set; get; }
        public string Action { set; get; }
        public string Ip { set; get; }
        public string LogInName { set; get; }
        [Key]
        [Column(Order = 2)]
        public DateTime UserActualDate { set; get; }
        
        public static void SaveUserLog(ref BEEContext context,UserLog userLog)
        {
            context.UserLog.Add(userLog);
        }
    }
}