using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using CatMash.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CatMash.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task<ActionResult> Index()
        {
            string url = "https://latelier.co/data/cats.json";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(jsonData);
                    JArray imagesArray = jsonObject["images"] as JArray;

                    List<Cat> cats = new List<Cat>();
                    foreach (JToken imageToken in imagesArray)
                    {
                        string id = imageToken["id"].ToString();
                        string imageUrl = imageToken["url"].ToString();

                        Cat cat = new Cat { Id = id, Url = imageUrl };
                        cats.Add(cat);
                    }

                    return View(cats);
                }
                else
                {
                    return View(Error);
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}