using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class AbsenceController : Controller
    {
        HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44381/api/")
        };

        [Route("/Absence")]
        public IActionResult Absence()
        {
            return View();
        }
        
        public async Task<JsonResult> Post(string userName)
        {
            var content = JsonConvert.SerializeObject(new { userName = userName });

            var postTask = await client.PostAsync("Absence", new StringContent(content, Encoding.UTF8, "application/json"));

            
            return Json(new { status = postTask.StatusCode, message = postTask.Content.ReadAsStringAsync().Result});
            
            
        }
    }
}