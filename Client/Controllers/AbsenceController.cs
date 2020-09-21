using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class AbsenceController : Controller
    {
        HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44341/api/")
        };

        [Route("/Absence")]
        public IActionResult Absence()
        {
            return View();
        }
        
        public async Task<JsonResult> Post(string userName)
        {
            var content = JsonConvert.SerializeObject(new { userName = userName });

            var postTask = await client.PostAsync("Absences", new StringContent(content, Encoding.UTF8, "application/json"));

            
            return Json(new { status = postTask.StatusCode, message = postTask.Content.ReadAsStringAsync().Result});
            
            
        }

        public async Task<JsonResult> Get()
        {
            List<Absence> absences = null;
            var getTask = await client.GetAsync("Absences");

            var content = getTask.Content.ReadAsStringAsync().Result;
            absences = JsonConvert.DeserializeObject<List<Absence>>(content);
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                absences = absences.Where(a => a.TimeIn.ToString("yyyy-MM-dd") == DateTimeOffset.Now.ToString("yyyy-MM-dd")).ToList();
            }
            
            return Json(absences);
        }

        public async Task<JsonResult> GetToday()
        {
            List<Absence> absences = null;
            var getTask = await client.GetAsync("Absences");

            var content = getTask.Content.ReadAsStringAsync().Result;
            absences = JsonConvert.DeserializeObject<List<Absence>>(content);
            
            absences = absences.Where(a => a.TimeIn.ToString("yyyy-MM-dd") == DateTimeOffset.Now.ToString("yyyy-MM-dd")).ToList();
            

            return Json(absences);
        }

        public async Task<JsonResult> GetRatio()
        {
            List<Absence> absences = null;
            List<Employee> employees = null;
            var employeeTask = await client.GetAsync("employees");
            var absenceTask = await client.GetAsync("absences");

            var content = employeeTask.Content.ReadAsStringAsync().Result;
            var content2 = absenceTask.Content.ReadAsStringAsync().Result;

            absences = JsonConvert.DeserializeObject<List<Absence>>(content2);
            employees = JsonConvert.DeserializeObject<List<Employee>>(content);

            absences = absences.Where(a => a.TimeIn.ToString("yyyy-MM-dd") == DateTimeOffset.Now.ToString("yyyy-MM-dd")).ToList();
            float ratio = (float) absences.Count / employees.Count;

            return Json(new { Count = ratio * 100});
        }

        public async Task<JsonResult> Check(string id)
        {
            List<Absence> absences = null;
            bool check = true;
            var getTask = await client.GetAsync("Absences");

            var content = getTask.Content.ReadAsStringAsync().Result;
            absences = JsonConvert.DeserializeObject<List<Absence>>(content);
            
            absences = absences.Where(a => a.TimeIn.ToString("yyyy-MM-dd") == DateTimeOffset.Now.ToString("yyyy-MM-dd")
            && a.Id == id).ToList();
            
            if (absences != null)
            {
                check = false;
            }

            return Json(new { check = check });
        }

        public async Task<JsonResult> PieChart()
        {
            List<Absence> absences = null;
            var getTask = await client.GetAsync("Absences");

            var content = getTask.Content.ReadAsStringAsync().Result;
            absences = JsonConvert.DeserializeObject<List<Absence>>(content);
            
            var query = absences.Where(a => a.TimeIn.ToString("yyyy-MM-dd") == DateTimeOffset.Now.ToString("yyyy-MM-dd"))
                .GroupBy(a => a.User.Employee.Divisions.Name)
                .Select(group => new
                {
                    Division = group.Key,
                    Count = group.Count()
                });
            

            return Json(query);
        }

        public async Task<JsonResult> BarChart()
        {
            List<Absence> absences = null;
            var getTask = await client.GetAsync("Absences");

            var content = getTask.Content.ReadAsStringAsync().Result;
            absences = JsonConvert.DeserializeObject<List<Absence>>(content);

            var query = absences
                .GroupBy(a => a.TimeIn.ToString("yyyy-MM-dd"))
                .Select(group => new
                {
                    DateIn = group.Key,
                    Count = group.Count()
                }).OrderBy(group => group.DateIn);


            return Json(query);
        }
    }

    public class Dto
    {
        public string id { get; set; }
    }
}