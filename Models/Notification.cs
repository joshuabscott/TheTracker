using System;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//ADD #12 Notification
namespace TheTracker.Models
{
    public class Notification
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Key
        [DisplayName("Project")]
        public int ProjectId { get; set; }

        [DisplayName("Ticket")]
        public int TicketId { get; set; }

        [Required]
        [DisplayName("Title")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [DisplayName("Message")]
        [StringLength(5000, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string Message { get; set; }

        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset Created { get; set; }

        //Foreign Key
        [Required]
        [DisplayName("Sender")]
        public string SenderId { get; set; }

        [Required]
        [DisplayName("Recipient")]
        public string RecipientId { get; set; }

        //public int NotificationTypeId { get; set; }

        [DisplayName("Has Been Viewed")]
        public bool Viewed { get; set; }

        //Virtual Nav. Properties
        public virtual TTUser Sender { get; set; }
        public virtual TTUser Recipient { get; set; }
        //public virtual NotificationType NotificationType { get; set; }

        public virtual ICollection<Project> Project { get; set; } = new HashSet<Project>();
        public virtual ICollection<Ticket> Ticket { get; set; } = new HashSet<Ticket>();
    }
}