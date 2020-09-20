using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using API.Models;

namespace Client.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44341/api/")           // localhost:44317
        };
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Load(int? Id)
        {
            IEnumerable<Department> departmentList = null;
            Department departments = null;

            var readTask = await client.GetAsync("Departments/" + Id);


            if(readTask.IsSuccessStatusCode)
            {
                var output = readTask.Content.ReadAsStringAsync().Result;
                if(Id != null)
                {
                    departments = JsonConvert.DeserializeObject<Department>(output);
                    return Json(departments);
                }
                departmentList = JsonConvert.DeserializeObject<List<Department>>(output);
            }
            return Json(departmentList);
        }

        public async Task<JsonResult> Insert(int? Id, Department department)
        {
            var item = JsonConvert.SerializeObject(department);

            if(Id != null)
            {
                var postTask = await client.PutAsync("departments/" + Id, new StringContent(item, Encoding.UTF8, "application/json"));
                return Json(new { success = postTask.IsSuccessStatusCode });
            }
            else
            {
                var postTask = client.PostAsync("departments", new StringContent(item, Encoding.UTF8, "application/json"));
                postTask.Wait();
                var result = postTask.Result;
                var oncom = result.Content.ReadAsStringAsync().Result;

                return Json(new { success = result.IsSuccessStatusCode });
            }
        }

        public async Task<JsonResult> Delete(int Id)
        {
            var deleteTask = await client.DeleteAsync("departments/" + Id);
            return Json(new { success = deleteTask.IsSuccessStatusCode });
        }
    }
}
