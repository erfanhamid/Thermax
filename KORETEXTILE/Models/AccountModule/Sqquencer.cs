using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.AccountModule
{
    [Table("Sqquencer")]
    public class Sqquencer
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int Refvalue { set; get; }
    }
}