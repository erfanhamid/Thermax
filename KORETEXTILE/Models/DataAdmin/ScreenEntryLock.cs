using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.Data_Admin
{
    [Table("ScreenEntryLock")]
    public class ScreenEntryLock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int ScreenID { get; set; }
        [Required]
        public DateTime SELDate { get; set; }
    }
}