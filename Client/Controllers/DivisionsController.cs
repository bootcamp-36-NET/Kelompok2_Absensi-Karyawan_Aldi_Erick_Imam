using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class DivisionsController : Controller
    {
        private readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44381/api/")
        };
        public IActionResult Index()
        {
            //if(HttpContext.Session.GetString("Role") == "Admin")
            //{
                return View();
            //}
            //return Redirect("/NotFound");
        }

        public async Task<JsonResult> Load (int? id)
        {
            IEnumerable<Divisions> divisionList = null;
            Divisions divisions = null;

            var readTask = client.GetAsync("Divisions/" + id);
            readTask.Wait();
            var result = readTask.Result;

            if(result.IsSuccessStatusCode)
            {
                var output = result.Content.ReadAsStringAsync().Result;
                if(id != null)
                {
                    divisions = JsonConvert.DeserializeObject<Divisions>(output);
                    return Json(divisions);
                }
                divisionList = JsonConvert.DeserializeObject<List<Divisions>>(output);
            }
            return Json(divisionList);
        }

        public async Task<JsonResult> Insert (int? Id, Divisions divisions)
        {
            var item = JsonConvert.SerializeObject(divisions);

            if(Id != null)
            {
                var postTask = client.PutAsync("divisions/" + Id, new StringContent(item, Encoding.UTF8, "application/json"));
                postTask.Wait();
                var result = postTask.Result;
                return Json(new { success = result.IsSuccessStatusCode });
            }
            else
            {
                var postTask = client.PostAsync("divisions/", new StringContent(item, Encoding.UTF8, "application/json"));
                postTask.Wait();
                var result = postTask.Result;
                var oncom = result.Content.ReadAsStringAsync().Result;
                return Json(new { success = result.IsSuccessStatusCode });
            }
        }

        public async Task<JsonResult> Delete(int Id)
        {
            var deleteTask = client.DeleteAsync("divisions/" + Id);
            deleteTask.Wait();
            var result = deleteTask.Result;

            return Json(new { success = result.IsSuccessStatusCode });
        }
    }
}
