using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IdentityFrame.Models
{
    public class StudioModel
    {
        
            [Key]
            [Required]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int StudioId { set; get; }
            public string StudioName { set; get; }
            public string StudioDescription { set; get; }
            public string TechDirector { set; get; }

            public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        
    }
}