using System.ComponentModel.DataAnnotations;

namespace Mvc.Controllers
{
    public class RegisterViewModel
    {
        
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Age")]
            public int Age { get; set; }

            [Required]
            [Display(Name = "Salary")]
            public double Salary { get; set; }

            [Required]
            [Display(Name = "Studio")]
            public string Studio { get; set; }

            [Required]
            [Display(Name = "Role")]
            public string Role { get; set; }




            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            //[DataType(DataType.Password)]
            //[Display(Name = "Confirm password")]
            //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            //public string ConfirmPassword { get; set; }
        }

    }
