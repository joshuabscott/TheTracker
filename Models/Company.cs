using System;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

//ADD #10 Company
namespace TheTracker.Models
{
    public class Company
    {
        #region Company
        //Primary Key
        public int Id { get; set; }

        [Required]
        [DisplayName("Company Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and a max {1} characters long.", MinimumLength = 2)]
        public string CompanyName { get; set; }

        [DisplayName("Company Description")]
        [StringLength(5000, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string Description { get; set; }
        #endregion

        #region Company Image
        [DisplayName("Company Logo")]
        public byte[] CompanyFileData { get; set; }
        public string CompanyFileType { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile CompanyFormFile { get; set; }
        #endregion

        //Virtual Nav. Properties
        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
        public virtual ICollection<TTUser> Members { get; set; } = new HashSet<TTUser>();
        public virtual ICollection<Invite> Invites { get; set; } = new HashSet<Invite>();
    }
}
