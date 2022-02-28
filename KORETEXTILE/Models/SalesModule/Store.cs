using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SalesModule
{
    [Table("Store")]
    public class Store
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public int DepotID { set; get; }
        [Required]
        public string Type { set; get; }
        [NotMapped]
        public string Depot { get; set; }
        //[Required]
        //public int FGStoreID { get; set; }


    }
}