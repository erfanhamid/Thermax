using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Procurement
{
    public class FreezerItemEntryVModel
    {
        public int Id { get; set; }
        [Required]
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public string ModelNameModify { get; set; }
        [Required]
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string BrandNameModify { get; set; }
        [Required]
        public int CapacityID { get; set; }
        public string Capacity { get; set; }
        public string CapacityNameModify { get; set; }
        [Required]
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public string TypeNameModify { get; set; }
        public int UnitID { get; set; }
        public string Description { get; set; }
    }
}