using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BEEERP.Models.DataAdmin.SPSettings
{
    [Table("SPBox")]
    public class SPBox
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int BoxID { get; set; }
        [Required]
        public string BoxNo { get; set; }
        [Required]
        public int StoreID { get; set; }

    }
}