﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class AccountsController : Controller
    {
        HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44381/api/")
        };

        [Route("/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login(LoginVM login)
        {
            var content = JsonConvert.SerializeObject(login);

            var postTask = await client.PostAsync("Logins", new StringContent(content, Encoding.UTF8, "application/json"));
            
            if (postTask.IsSuccessStatusCode)
            {
                return RedirectToAction("", "Home");
            }
            //return Redirect("/Login");

            ViewData["Validation"] = postTask.Content.ReadAsStringAsync().Result;
            return View();
        }

        

    }
}