using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class StudioViewModel
    {
        
        [Required]
        [Key]
        public int StudioId { set; get; }
        public string StudioName { set; get; }
        public string StudioDescription { set; get; }
        public string TechDirector { set; get; }
    }
}