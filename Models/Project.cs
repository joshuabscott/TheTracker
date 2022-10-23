using System;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

//ADD #08 Project & Project Priority (part 1)
namespace TheTracker.Models
{
    public class Project
    {
        #region Project
        //Primary Key
        public int Id { get; set; }

        //Foreign Key
        [DisplayName("Company")]
        public int CompanyId { get; set; }

        [Required]
        [DisplayName("Project Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Description")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        [DisplayName("Created")]
        [DataType(DataType.Date)]
        public DateTimeOffset Created { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset StartDate { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset EndDate { get; set; }

        //Foreign Key
        [DisplayName("Priority")]
        public int ProjectPriorityId { get; set; }

        [DisplayName("Archived")]
        public bool Archived { get; set; }
        #endregion

        #region Project Image
        [DisplayName("File Name")]
        public byte[] ProjectFileData { get; set; }
        public string ProjectFileType { get; set; }

        [DisplayName("File Extension")]
        public string ImageContentType { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile ProjectFormFile { get; set; }
        #endregion

        //Virtual Nav. Properties
        public virtual Company Company { get; set; }
        public virtual ProjectPriority ProjectPriority { get; set; }

        public virtual ICollection<TTUser> Members { get; set; } = new HashSet<TTUser>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
