using System;
using System.Collections.Generic;

using System.IdentityModel.Tokens.Jwt;

using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Client.ViewModels;

using API.ViewModel;
using Microsoft.AspNetCore.Http;

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
        public async Task<IActionResult> Login(LoginVm login)

        {
            var content = JsonConvert.SerializeObject(login);

            var postTask = await client.PostAsync("Logins", new StringContent(content, Encoding.UTF8, "application/json"));
            
            if (postTask.IsSuccessStatusCode)
            {

                var jwt = postTask.Content.ReadAsStringAsync().Result;
                Session(jwt);

                return RedirectToAction("", "Home");
            }
            //return Redirect("/Login");

            ViewData["Validation"] = postTask.Content.ReadAsStringAsync().Result;
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("", "Home");
        }

        public void Session(string stream)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokens = handler.ReadToken(stream) as JwtSecurityToken;
            var id = tokens.Claims.First(claim => claim.Type == "Id").Value;
            var userName = tokens.Claims.First(claim => claim.Type == "UserName").Value;
            var role = tokens.Claims.First(claim => claim.Type == "Role").Value;
            var email = tokens.Claims.First(claim => claim.Type == "Email").Value;
            var phone = tokens.Claims.First(claim => claim.Type == "Phone").Value;

            var auth = "Bearer " + stream;

            HttpContext.Session.SetString("Id", id);
            HttpContext.Session.SetString("UserName", userName);
            HttpContext.Session.SetString("Role", role);
            HttpContext.Session.SetString("Email", email);
            HttpContext.Session.SetString("Phone", phone);

            HttpContext.Session.SetString("Auth", auth);
        }
    }
}