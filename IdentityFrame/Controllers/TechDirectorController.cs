using IdentityFrame.Models;
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

    [RoutePrefix("api/TechDirector")]
    public class TechDirectorController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationSignInManager _signInManager;

        public TechDirectorController()
        {

        }


        public TechDirectorController(ApplicationUserManager userManager, ApplicationRoleManager roleManager
           , ApplicationSignInManager signInManager)
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
        [JwtAuthentication(Role = "TD")]
        [Route("GetEmployees")]
        public IHttpActionResult GetEmployees()
        {

          //  var  = UserManager.Users.ToList();

            // UserManager.Users.Where( u => !u.Role.Equals("TD") && u.StudioName.Equals("") );// where role is not TD
            var Employees = UserManager.Users.Where(u => !u.Role.Equals("TD")).ToList();



            if (Employees != null)
            {
                return Ok(Employees);
            }
            else
            {
                return NotFound();
            }
        }


        [AllowAnonymous]
        [JwtAuthentication(Role = "TD")]
        //  [Route("UpdateEmployee/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateEmployee(string id, ApplicationUser applicationUser)
        {
            var employee = UserManager.FindById(applicationUser.Id);
            if (employee == null)
            {

                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No id with that task={0}", id)),
                    ReasonPhrase = "Not Found"
                };
                throw new HttpResponseException(resp);

            }
            
            //employee.Role = applicationUser.Role;    
            employee.StudioName = applicationUser.StudioName;
            UserManager.Update(employee);
            return Ok();
        }



    }
}
