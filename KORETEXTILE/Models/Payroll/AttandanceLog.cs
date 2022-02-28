using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    public class AttandanceLog
    {
        public int Id { set; get; }
        public int EmployeeId { set; get; }
        public DateTime CheckIn { set; get; }
        public DateTime CheckOut { set; get; }
    }
}