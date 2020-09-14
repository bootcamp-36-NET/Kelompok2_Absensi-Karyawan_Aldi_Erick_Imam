using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AccountsController : Controller
    {
        HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:12345/api/")
        };

        [Route("/Login")]
        public IActionResult Login()
        {
            return View();
        }

    }
}