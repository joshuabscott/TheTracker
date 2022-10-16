using System;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//ADD #02 TicketType, TicketStatus & TicketPriority
namespace TheTracker.Models
{
    public class TicketType
    {
        //Primary Key
        public int Id { get; set; }

        [DisplayName("Ticket Type")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }
    }
}
