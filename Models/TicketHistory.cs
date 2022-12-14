using System;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//ADD #03 TicketHistory
namespace TheTracker.Models
{
    public class TicketHistory
    {
        //Primary Key
        public int Id { get; set; }

        [DisplayName("Ticket")]
        public int TicketId { get; set; }

        [DisplayName("Property Name")]
        public string Property { get; set; }

        [DisplayName("Description of Change")]
        [StringLength(5000, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        [DisplayName("Date Modified")]
        [DataType(DataType.Date)]
        public DateTimeOffset Created { get; set; }

        [DisplayName("Previous")]
        public string OldValue { get; set; }

        [DisplayName("Current")]
        public string NewValue { get; set; }

        [Required]
        [DisplayName("Team Member")]
        public string UserId { get; set; }

        //Virtual Nav. Properties
        public virtual Ticket Ticket { get; set; }
        public virtual TTUser User { get; set; }
    }
}