using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
//ADD #05 TicketComment (part 1)
namespace TheTracker.Models
{
    public class TicketComment
    {
        //ADD #06 TicketComment (part 2)
        //Primary Key
        public int Id { get; set; }

        [Required]
        [DisplayName("Member Comment")]
        [StringLength(5000, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string Comment { get; set; }

        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset Created { get; set; }

        [DisplayName("Ticket")]
        public int TicketId { get; set; }

        [DisplayName("Team Member")]
        public string UserId { get; set; }

        //Virtual Nav. Properties
        public virtual Ticket Ticket { get; set; }
        public virtual TTUser User { get; set; }
    }
}
