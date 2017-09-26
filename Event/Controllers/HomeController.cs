using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Event.Models;
using Microsoft.AspNetCore.Authorization;
using Event.Models.Respone.Item;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Event.Controllers
{

    public class HomeController : Controller
    {
        HttpClient client;
        string url = "http://localhost:54443";

        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        [Authorize]
        public async Task<IActionResult> Index(IndexViewModel indexViewModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            indexViewModel.Username = user.UserName;

            HttpResponseMessage Response = await client.GetAsync("api/Point/getPointsValue=" + user.Id);

            if (Response.IsSuccessStatusCode)
            {
                var pointResponse = Response.Content.ReadAsStringAsync().Result;

                var pointsResponse = JsonConvert.DeserializeObject<GetPointsValueResponse>(pointResponse);

                indexViewModel.Points = pointsResponse.Points;
            }
                return View(indexViewModel);
            
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
