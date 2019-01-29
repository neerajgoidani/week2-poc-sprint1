using IdentityFrame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class TechDirectorMvcController : Controller
    {
        static HttpClient client = new HttpClient()
        {
          
        };



        // GET: TechDirector
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetEmployees(RegisterViewModel model)
        {
            string tokenValue = (string)Session["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenValue);

            HttpResponseMessage responseMessage = client.GetAsync("http://localhost:50581/api/TechDirector/GetEmployees").Result;


            if (responseMessage.IsSuccessStatusCode == true)
            {
                List<ApplicationUser> employeeList = responseMessage.Content.ReadAsAsync<List<ApplicationUser>>().Result;

                return View(employeeList);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }



        [HttpPost]
        [AllowAnonymous]
        public ActionResult EditEmployee(ApplicationUser employee)
        {

            try
            {

                HttpResponseMessage response = client.PutAsJsonAsync("http://localhost:50581/api/TechDirector/UpdateEmployee" + employee.Id, employee).Result;
                bool xyz = response.IsSuccessStatusCode;
                if (xyz)
                    return RedirectToAction("GetEmployees");
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch (Exception exe)
            {
                return RedirectToAction("Login");
                //   return View();
            }

        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult EditEmployee(string id)
        {
            ApplicationUser employee = null;
            try
            {

                // HttpResponseMessage response = client.GetAsync($"api/employee/employee/{id}").Result;
                HttpResponseMessage response = client.GetAsync($"http://localhost:50581/api/Employee/GetEmployee/{id}").Result;
                response.EnsureSuccessStatusCode();

                HttpResponseMessage responseMessage = client.GetAsync("http://localhost:50581/api/StudioApi/GetStudioList").Result;
                

                if (responseMessage.IsSuccessStatusCode == true)
                {

                    IEnumerable<StudioModel> StudioList = responseMessage.Content.ReadAsAsync<IEnumerable<StudioModel>>().Result;
                    ViewData["MyStudioList"] = new SelectList(StudioList, "StudioName", "StudioName");

                }


                if (response.IsSuccessStatusCode)
                {

                    employee = response.Content.ReadAsAsync<ApplicationUser>().Result;
                    return View(employee);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch (Exception exe)
            {
                return RedirectToAction("Login");
                // return View();
            }

        }



    }
}