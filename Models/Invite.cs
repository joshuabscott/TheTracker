using System;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//ADD #13 Invite
namespace TheTracker.Models
{
    public class Invite
    {
        //Primary Key
        public int Id { get; set; }

        [DisplayName("Date Sent")]
        [DataType(DataType.Date)]
        public DateTimeOffset InviteDate { get; set; }

        [DisplayName("Join Sent")]
        [DataType(DataType.Date)]
        public DateTimeOffset JoinDate { get; set; }

        [DisplayName("Code")]
        public Guid CompanyToken { get; set; }

        //Foreign Key
        [DisplayName("Company")]
        public int CompanyId { get; set; }

        [DisplayName("Project")]
        public int ProjectId { get; set; }

        [Required]
        [DisplayName("Inviter")]
        public string InviterId { get; set; }

        [DisplayName("Invitee")]
        public string InviteeId { get; set; }

        [Required]
        [DisplayName("Invitee Email")]
        public string InviteeEmail { get; set; }

        [Required]
        [DisplayName("Invitee First Name")]
        public string InviteeFirstName { get; set; }

        [Required]
        [DisplayName("Invitee Last Name")]
        public string InviteeLastName { get; set; }

        [StringLength(5000, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string Message { get; set; }

        public bool IsValid { get; set; }

        //Virtual Nav. Properties
        public virtual Company Company { get; set; }
        public virtual Project Project { get; set; }
        public virtual TTUser Inviter { get; set; }
        public virtual TTUser Invitee { get; set; }
    }
}
