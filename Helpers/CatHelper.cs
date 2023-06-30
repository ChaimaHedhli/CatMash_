using CatMash.Models;
using Newtonsoft.Json.Linq;
using System.Net;

namespace CatMash.Helpers
{
    public static class CatHelper
    {
        public static List<Cat> GetCatsFromJson()
        {
            string url = "https://latelier.co/data/cats.json";
            using (var webClient = new WebClient())
            {
                List<Cat> cats = new List<Cat>();
                string json = webClient.DownloadString(url);

                    JObject jsonObject = JObject.Parse(json);
                    JArray catsArray = jsonObject["cats"] as JArray;

                    foreach (JObject item in catsArray) 
                    {
                    string id = item.GetValue("id").ToString();
                    string imageUrl = item.GetValue("url").ToString();

                    Cat cat = new Cat { Id = id, Url = imageUrl };
                    cats.Add(cat);
                }
                    return cats;
       
            }
        }
    }
}
