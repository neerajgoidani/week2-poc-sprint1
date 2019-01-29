using IdentityFrame.Models;
using Mvc.Models;
using Mvc.Views.AccountMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class EmployeeMvcController : Controller
    {
       static HttpClient client = new HttpClient();
       //static HttpClient client2 = new HttpClient();
        // GET: AccountMvc
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            

                HttpResponseMessage responseMessage = client.GetAsync("http://localhost:50581/api/StudioApi/GetStudioList").Result;
                HttpResponseMessage responseMessages = client.GetAsync("http://localhost:50581/api/Employee/GetRoleList").Result;
                if (responseMessage.IsSuccessStatusCode == true  && responseMessages.IsSuccessStatusCode)
                {
                   
                    IEnumerable<StudioModel> StudioList = responseMessage.Content.ReadAsAsync<IEnumerable<StudioModel>>().Result;
                    ViewData["MyStudioList"] = new SelectList(StudioList, "StudioName", "StudioName");

                    IEnumerable<RoleViewModel> RoleList = responseMessages.Content.ReadAsAsync<IEnumerable<RoleViewModel>>().Result;
                    ViewData["MyRoleList"] = new SelectList(RoleList, "Name", "Name");
                 
                    return View();
                }
                else
                {
                    return View();// wrong 
                }

            
               
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult EditEmployee(ApplicationUser employee)
        {

            try
            {

                HttpResponseMessage response = client.PutAsJsonAsync("http://localhost:50581/api/employee/" + employee.Id, employee).Result;
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



        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel employee)
        {

            try
            {
                
                    HttpResponseMessage response = client.PostAsJsonAsync("http://localhost:50581/api/Employee/Login" , employee).Result;

                    bool xyz = response.IsSuccessStatusCode;

                if (xyz == true)
                {
                    string tokenValue = response.Content.ReadAsAsync<String>().Result;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenValue);
                    Session["Token"] = tokenValue;

                    HttpResponseMessage response1 = client.GetAsync($"http://localhost:50581/api/Employee/GetEmployeeByName/{employee.Name}").Result;


                    if (response1.IsSuccessStatusCode)
                    {

                        ApplicationUser user = response1.Content.ReadAsAsync<ApplicationUser>().Result;
                        if ((user.Role).Equals("HR"))
                        {
                            return RedirectToAction("GetEmployees");
                        }
                        else if ((user.Role).Equals("Admin"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if ((user.Role).Equals("TD"))
                        {
                            return RedirectToAction("GetEmployees", "TechDirectorMvc");
                        }


                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                  

                    return RedirectToAction("Login");

                }
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
                HttpResponseMessage responseMessages = client.GetAsync("http://localhost:50581/api/Employee/GetRoleList").Result;

                if (responseMessage.IsSuccessStatusCode == true && responseMessages.IsSuccessStatusCode)
                {

                    IEnumerable<StudioModel> StudioList = responseMessage.Content.ReadAsAsync<IEnumerable<StudioModel>>().Result;
                    ViewData["MyStudioList"] = new SelectList(StudioList, "StudioName", "StudioName");

                    IEnumerable<RoleViewModel> RoleList = responseMessages.Content.ReadAsAsync<IEnumerable<RoleViewModel>>().Result;
                    ViewData["MyRoleList"] = new SelectList(RoleList, "Name", "Name");

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



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {


           
            
                HttpResponseMessage response = client.PostAsJsonAsync("http://localhost:50581/api/Employee/Register", model).Result;
                bool xyz = response.IsSuccessStatusCode;
                if (xyz == true)
                {
                    //bool xyz = response.IsSuccessStatusCode;
                    //string tokenValue = response.Content.ReadAsAsync<String>().Result;
                    //client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenValue);
                    // authenticToken = combinedValue;
                  //  Session["Token"] = tokenValue;
                    return RedirectToAction("Login");
                }
                else
                {
                    return RedirectToAction("Register");
                }
             

        }

        // public ActionResult EmployeeDetails(string id)
        //{


        //  ApplicationUser employee = null;
        //try
        // {


        //   HttpResponseMessage response = client.GetAsync("http://localhost:50581/api/Employee/GetEmployee/",id).Result;
        // if (response.IsSuccessStatusCode)
        // {

        //   employee = response.Content.ReadAsAsync<ApplicationUser>().Result;
        // return View(employee);
        // }
        // else
        //  {
        //    return RedirectToAction("Login");
        //  }


        //  }
        //  catch (Exception exe)
        //  {
        //    return RedirectToAction("Login");
        // }


        // }
        

        [HttpGet]
        [AllowAnonymous]       
        public ActionResult GetEmployees(RegisterViewModel model)
        {
           
            HttpResponseMessage responseMessage = client.GetAsync("http://localhost:50581/api/Employee/GetEmployees").Result;


            if (responseMessage.IsSuccessStatusCode == true)
            {
                List<ApplicationUser> employeeList = responseMessage.Content.ReadAsAsync<List<ApplicationUser>>().Result;

                return View(employeeList);
            }
            else
            {
                return RedirectToAction("Login");
            }

        }


        
        [AllowAnonymous]
        public ActionResult DeleteEmployee(string id)
        {
            try
            {

                HttpResponseMessage response = client.DeleteAsync($"http://localhost:50581/api/Employee/DeleteEmployeeById/{id}").Result;
                bool xyz = response.IsSuccessStatusCode;
                if (xyz == true)
                {
                    return RedirectToAction("GetEmployees");
                }
                else
                {
                    return RedirectToAction("GetEmployees");
                }
            }
            catch (Exception exe)
            {
                return RedirectToAction("Login");
                //  return View();
            }

        }

        [AllowAnonymous]
        public ActionResult EmployeeDetails(string id)
        {


            ApplicationUser employee = null;
            try
            {


                HttpResponseMessage response = client.GetAsync($"http://localhost:50581/api/Employee/GetEmployee/{id}").Result;
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
            }


        }








    }
}