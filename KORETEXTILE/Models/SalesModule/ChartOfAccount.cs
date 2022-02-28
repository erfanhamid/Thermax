using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.SalesModule
{
    [Table("ChartOfAccount")]
    public class ChartOfAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }
        public string Name { get; set; }
        [Column("parentId")]
        public int ParentId { get; set; }
        [Column("type")]
        public string Type { get; set; }
        [Column("initialBalanceTransectionId ")]
        public int InitialBalanceTransectionId { get; set; }
        [Column("acctype")]
        public string Acctype { get; set; }
        [Column("rootAccountType")]
        public string RootAccountType { get; set; }
    }
}