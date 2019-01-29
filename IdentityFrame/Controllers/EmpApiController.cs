using IdentityFrame.Models;
using IdentityFrame.Repository;
using IdentityFrame.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IdentityFrame.Controllers
{
    public class EmpApiController : ApiController
    {
        StudioRepository repository = new StudioRepository();
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationSignInManager _signInManager;

        ApplicationDbContext db = new Models.ApplicationDbContext();

        public EmpApiController()
        {
        }

        public EmpApiController(ApplicationUserManager userManager, ApplicationRoleManager roleManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            
            RoleManager = roleManager;
            SignInManager = signInManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? Request.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }


        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        [AllowAnonymous]
        [Route("api/EmpApi/GetEmployeeByName/{name}")]
        [JwtAuthentication(Role ="Employee")]
        public IHttpActionResult GetEmployeeByName(string name)
        {

            var user = UserManager.FindByName(name);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }


        }

        [HttpGet]
        [Route("api/EmpApi/GetStudioByName/{id}")]
        [JwtAuthentication(Role ="Employee")]
        public IHttpActionResult GetStudioByName(string id)
        {
            StudioModel studios = repository.GetStudio(id);
            if (studios != null)
            {
                //   log.Info("Studio Retrieved Successfully");
                return Ok(studios);
            }
            else
            {
                // log.Error("Studio could not be found");
                return NotFound();
            }


        }
    }
}
