using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using System.Web.Routing;

namespace IdentityFrame.Security
{
    public class JwtAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public string Role { get; set; }

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            if (filterContext.Request.Headers.Authorization.Parameter == null)
            {
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authorizatonToken = filterContext.Request.Headers.Authorization.Parameter;
             

                string userRole = AuthenticationModule.ValidateToken(authorizatonToken); // we will get the user role

                if (Role == userRole)
                {
                    
                }
                else
                {
                    filterContext.Response= filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    //filterContext.Response = filterContext.Request.CreateResponse();
                    //filterContext.Response.Content = new StringContent(userRole);
                }
                //else if (userRole == "Admin")
                //{



                //    //var response = filterContext.Request.CreateResponse(System.Net.HttpStatusCode.Moved);
                //    //response.Headers.Location = new Uri("http://www.abcmvc.com");

                //    //var response = filterContext.Request.CreateResponse(System.Net.HttpStatusCode.Redirect);
                //    //response.Headers.Location = new Uri("http://localhost:50665/Admin/index");

                //    //filterContext.Response = response;

                //    ////  filterContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized).Content;
                //}


                //   response.Content = new StringContent(userRole);
                //if (usrname.Equals(tokenUserName))
                //{

                //    //   actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
                //}
                //else
                //{
                //    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                //}


            }

        }
    }
}
