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
        string tokenValue,userName;
         



        // GET: TechDirector
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetEmployees(RegisterViewModel model)
        {
            tokenValue = (string)Session["Token"];
           

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenValue);

            HttpResponseMessage responseMessage = client.GetAsync("http://localhost:50581/api/TechDirector/GetEmployees").Result;


            if (responseMessage.IsSuccessStatusCode == true)
            {
                List<ApplicationUser> employeeList = responseMessage.Content.ReadAsAsync<List<ApplicationUser>>().Result;

                return View(employeeList);
            }
            else
            {
                return RedirectToAction("Login","EmployeeMvc");
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
                    return RedirectToAction("Login", "EmployeeMvc");
                }
            }
            catch (Exception exe)
            {
                return RedirectToAction("Login", "EmployeeMvc");
                //   return View();
            }

        }

        [AllowAnonymous]
        [HttpGet] // request on clicking a link is HttpGet
        public ActionResult EditEmployee(string id)
        {
            ApplicationUser employee = null;
            ApplicationUser techDirector = null;
            try
            {
                userName = (string)Session["UserName"];

                // HttpResponseMessage response = client.GetAsync($"api/employee/employee/{id}").Result;
                HttpResponseMessage response = client.GetAsync($"http://localhost:50581/api/Employee/GetEmployee/{id}").Result;
                response.EnsureSuccessStatusCode();
                employee = response.Content.ReadAsAsync<ApplicationUser>().Result;


                HttpResponseMessage response1 = client.GetAsync($"http://localhost:50581/api/Employee/GetEmployeeByName/{userName}").Result;
                response1.EnsureSuccessStatusCode();
                techDirector = response1.Content.ReadAsAsync<ApplicationUser>().Result;



                HttpResponseMessage responseMessage = client.GetAsync("http://localhost:50581/api/StudioApi/GetStudioList").Result;
                

                if (responseMessage.IsSuccessStatusCode == true)
                {

                    List<StudioModel> fullStudioList = responseMessage.Content.ReadAsAsync<List<StudioModel>>().Result;

                    List<StudioModel> optionsStudioList = new List<StudioModel>();

                    foreach (StudioModel studio in fullStudioList)
                    {
                        if (studio.StudioName == employee.StudioName || studio.StudioName == techDirector.StudioName)
                        {
                            optionsStudioList.Add(studio);
                        }
                        else
                        {

                        }

                    }



                    ViewData["MyStudioList"] = new SelectList(optionsStudioList, "StudioName", "StudioName");

                }


                if (response.IsSuccessStatusCode)
                {

                  //  employee = response.Content.ReadAsAsync<ApplicationUser>().Result;
                    return View(employee);
                }
                else
                {
                    return RedirectToAction("Login", "EmployeeMvc");
                }
            }
            catch (Exception exe)
            {
                return RedirectToAction("Login", "EmployeeMvc");
                // return View();
            }

        }



    }
}