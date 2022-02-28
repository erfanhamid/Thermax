using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BEEERP.Models.Payroll
{
    [Table("EmployeeImageAndAttachment")]
    public class EmployeeImageAndAttachment
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        [NotMapped]
        public HttpPostedFileBase employeeImage { get; set; }
        public string EmployeeImageName { get; set; }
        [NotMapped]
        public HttpPostedFileBase AttachmentImage1 { get; set; }
        public string Attachment1 { get; set; }
        [NotMapped]
        public HttpPostedFileBase AttachmentImage2 { get; set; }
        public string Attachment2 { get; set; }
        [NotMapped]
        public HttpPostedFileBase AttachmentImage3 { get; set; }
        public string Attachment3 { get; set; }
        [NotMapped]
        public HttpPostedFileBase AttachmentImage4 { get; set; }
        public string Attachment4 { get; set; }
        [NotMapped]
        public HttpPostedFileBase AttachmentImage5 { get; set; }
        public string Attachment5 { get; set; }

    }
}