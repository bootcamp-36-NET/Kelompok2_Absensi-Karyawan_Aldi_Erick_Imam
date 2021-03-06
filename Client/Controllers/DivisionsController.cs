﻿using System;
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
            BaseAddress = new Uri("https://localhost:44341/api/")
        };
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                return View();
            }
            return NotFound();
        }

        public async Task<JsonResult> Load (int? id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("Auth"));
            IEnumerable<Divisions> divisionList = null;
            Divisions divisions = null;

            var readTask = await client.GetAsync("Divisions/" + id);


            if(readTask.IsSuccessStatusCode)
            {
                var output = readTask.Content.ReadAsStringAsync().Result;
                if(id != null)
                {
                    divisions = JsonConvert.DeserializeObject<Divisions>(output);
                    return Json(divisions);
                }
                divisionList = JsonConvert.DeserializeObject<List<Divisions>>(output);
            }
            return Json(divisionList);
        }

        public async Task<JsonResult> Insert(int? Id, Divisions divisions)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("Auth"));
            var item = JsonConvert.SerializeObject(divisions);

            if(Id != null)
            {
                var postTask = await client.PutAsync("divisions/" + Id, new StringContent(item, Encoding.UTF8, "application/json"));
                return Json(new { success = postTask.IsSuccessStatusCode });
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
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("Auth"));
            var deleteTask = await client.DeleteAsync("divisions/" + Id);

            return Json(new { success = deleteTask.IsSuccessStatusCode });
        }
    }
}
