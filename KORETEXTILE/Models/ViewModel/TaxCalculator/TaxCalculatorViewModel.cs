using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.TaxCalculator
{
    public class TaxCalculatorViewModel
    {
        [Display(Name = "Assesment Year")]
        public DateTime AssesmentYear { get; set; }
        [Display(Name = "Income During Year")]
        public DateTime DuringYear { get; set; }
        public string Location { get; set; }
        public int Corpuration { get; set; }
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }
    }
}