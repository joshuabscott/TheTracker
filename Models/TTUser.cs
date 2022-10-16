using System;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

//ADD #01 Intro
namespace TheTracker.Models
{
    public class TTUser : IdentityUser
    {
        #region First & Last Name = FullName
        [Required]
        [DisplayName("First Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at lest {2} and a max {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at lest {2} and a max {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }
        
        [NotMapped]
        [DisplayName("Full Name")]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        //Foreign Key
        public int CompanyId { get; set; }
        #endregion

        #region Project Member Avatar Image
        //ADD #11 ProjectMembers
        [DisplayName("Avatar")]
        public byte[] AvatarFileData { get; set; }
        public string AvatarFileType { get; set; }
        
        [DisplayName("File Extension")]
        public string AvatarContentName { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile AvatarFormFile { get; set; }
        #endregion

        //Virtual Nav. Properties
        public virtual Company Company { get; set; }
        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
