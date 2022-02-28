using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Database
{
    public class TaxContext: DbContext
    {
        public TaxContext() : base("connectionTax")    
        {

        }
        public static TaxContext Create()
        {
            return new TaxContext();
        }

    }
}