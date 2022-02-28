using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Log
{
    public static class ScreenAction
    {
        public static string Save
        {

            private set
            {

            }
            get
            {
                return "Save";
            }


        }
        public static string Update
        {
            private set
            {
               
            }

            get
            {
                return "Update";
            }
        }
        public static string Delete
        {
            private set
            {
               
            }

            get
            {
                return "Delete";
            }
        }
        public static string Approve
        {
            private set
            {
            }

            get
            {
                return "Approve";
            }
        }
    }
}