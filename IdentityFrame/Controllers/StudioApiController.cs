using IdentityFrame.Models;
using IdentityFrame.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IdentityFrame.Controllers
{
    public class StudioApiController : ApiController
    {
        StudioRepository repository = new StudioRepository();


        [HttpGet]
        [AllowAnonymous]
       // [Route("GetStudioList")]
        public IHttpActionResult GetStudioList()
        {
            IEnumerable<StudioModel> studios = repository.GetStudios();
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

        [HttpGet]
        [Route("api/StudioApi/GetStudioByName/{id}")]

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
