using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommonInformation
{
    public static class ServerInformation
    {
        static ServerInformation()
        {
            System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();

            builder.ConnectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString; 

            ServerName = builder["Data Source"] as string;
            DataBaseName = builder["Initial Catalog"] as string;
            UserId = builder["User ID"] as string;
            Password = builder["Password"] as string;
           
        }
        public static string ServerName { set; get; }
        public static string DataBaseName { set; get; }
        public static string UserId { set; get; }
        public static string Password { set; get; }
    }
}