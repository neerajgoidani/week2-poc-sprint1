using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IdentityFrame.Controllers
{
    public class AdminController : ApiController
    {
        public IHttpActionResult Test()
        {
            return Ok("admin");
        }
    }
}
