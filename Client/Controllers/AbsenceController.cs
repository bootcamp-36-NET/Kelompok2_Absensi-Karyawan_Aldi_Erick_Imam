using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Absence")]
        public async Task<IActionResult> SendAbsence(string userName)
        {
            var content = JsonConvert.SerializeObject(userName);

            var postTask = await client.PostAsync("Absence", new StringContent(content, Encoding.UTF8, "application/json"));

            if (postTask.IsSuccessStatusCode)
            {
                return View();
            }
            return View();
        }
    }
}