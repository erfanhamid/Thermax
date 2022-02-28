using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Store_FG
{
    [Table("ShiftInventory")]
    public class ShiftInventory
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SINo { get; set; }
        public DateTime SDate { get; set; }
        [Required]
        public int CurrentStoreID { get; set; }
        [Required]
        public int NewStoreID { get; set; }
        public string Description { get; set; }
    }
}