using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoinView.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using CoinView.Data;

namespace CoinView.Controllers {
    public class HomeController : Controller {

        public IActionResult Index() {
            return View();
        }

        public JsonResult GetData(int userID) {
            List<User> users = UserFactory.All();
            User user = users.Single(u => u.ID == userID);

            Dictionary<string, object> data = new Dictionary<string, object>();
            data["Users"] = users;
            data["Buys"] = DataCreator.GetBuys(user);
            data["Investments"] = DataCreator.GetInvestments(user);
            data["Creations"] = DataCreator.GetCreations(user);
            data["Trades"] = DataCreator.GetTrades(user);

            return Json(data);
        }

        public JsonResult GetCoinValues() {
            Dictionary<string, Coin> coins = CoinFactory.All().ToDictionary(c => c.CoinMarketCapID);
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = new Uri("https://api.coinmarketcap.com");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var result = client.GetStringAsync("/v1/ticker/?convert=EUR&limit=500").Result;
                Dictionary<int, CoinMarketCapResult> values = JsonConvert.DeserializeObject<List<CoinMarketCapResult>>(result).Where(c => coins.ContainsKey(c.ID)).ToDictionary(c => coins[c.ID].ID);
                return Json(values);
            }
        }

        public IActionResult Error() {
            return View();
        }
    }
}
