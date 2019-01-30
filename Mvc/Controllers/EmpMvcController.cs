using IdentityFrame.Models;
using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class EmpMvcController : Controller
    {
       static HttpClient client = new HttpClient();
        public ActionResult GetEmployee()
        {
            string tokenValue = (string)Session["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenValue);
            string name = (string)Session["Name"];

            ApplicationUser employee = null;
            try
            {


                HttpResponseMessage response = client.GetAsync($"http://localhost:50581/api/EmpApi/GetEmployeeByName/{name}").Result;
                if (response.IsSuccessStatusCode)
                {

                    employee = response.Content.ReadAsAsync<ApplicationUser>().Result;
                    
                    return View(employee);
                }
                else
                {
                    return RedirectToAction("Login","EmployeeMvc");
                }


            }
            catch (Exception exe)
            {
                return RedirectToAction("Login", "EmployeeMvc");
            }

         
        }

        public ActionResult GetStudio(string id)
        {

            string tokenValue = (string)Session["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenValue);

            StudioViewModel Studio = null;
            try
            {


                HttpResponseMessage response = client.GetAsync($"http://localhost:50581/api/EmpApi/GetStudioByName/{id}").Result;
                if (response.IsSuccessStatusCode)
                {

                    Studio = response.Content.ReadAsAsync<StudioViewModel>().Result;
                    return View(Studio);
                }
                else
                {
                    return RedirectToAction("Login");
                }


            }
            catch (Exception exe)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}