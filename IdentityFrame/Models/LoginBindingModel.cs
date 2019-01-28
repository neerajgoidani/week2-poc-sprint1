using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IdentityFrame.Models
{
    public class LoginBindingModel
    {
        

            [Required]
            [Display(Name = "Name")]
           // [EmailAddress]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }



       
    }
}