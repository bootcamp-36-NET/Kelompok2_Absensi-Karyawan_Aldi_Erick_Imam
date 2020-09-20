using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using Client.Models;
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
            return Json(absences);
        }
    }
}