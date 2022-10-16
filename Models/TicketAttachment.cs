using System;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

//ADD #04 TicketAttachment
namespace TheTracker.Models
{
    public class TicketAttachment
    {
        #region Ticket Attachment
        //Primary Key
        public int Id { get; set; }

        [DisplayName("File Description")]
        [StringLength(5000, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        [DisplayName("File Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset Created { get; set; }

        [DisplayName("Ticket #")]
        public int TicketId { get; set; }

        [DisplayName("Team Member")]
        public string UserId { get; set; }
        #endregion

        #region Ticket Image
        [DisplayName("File Name")]
        public byte[] TicketFileData { get; set; }
        public string TicketFileType { get; set; }
        public string TicketFileName { get; set; }

        [DisplayName("File Extension")]
        public string TicketContentType { get; set; }

        [NotMapped]
        [DisplayName("Select A File")]
        [DataType(DataType.Upload)]
        //[MaxFileSize(1024 * 1024)]
        //[AllowedExtensions(new string[] { ".jpg", ".png", ".doc", ".docx", ".xls", ".xlsx", ".pdf", ".txt" })]
        public IFormFile TicketFormFile { get; set; }
        #endregion

        //Virtual Nav. Properties
        public virtual Ticket Ticket { get; set; }
        public virtual TTUser User { get; set; }
    }
}
