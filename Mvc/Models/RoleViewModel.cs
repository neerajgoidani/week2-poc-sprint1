using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Views.AccountMvc
{
    public class RoleViewModel
    {
        [Key]
        public string RolesId { set; get; }
        public string Name { set; get; }
    }
}