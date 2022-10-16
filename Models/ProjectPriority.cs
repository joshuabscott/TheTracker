using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//ADD #09 Project & Project Priority (part 2)
namespace TheTracker.Models
{
    public class ProjectPriority
    {
        //Primary Key
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }
    }
}
