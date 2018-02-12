using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoinView.Models;
using CoinView.Models.Database;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CoinView.Controllers {
    public class HomeController : Controller {

        private readonly CoinViewContext db;

        public HomeController(CoinViewContext _context) {
            db = _context;
        }

        public JsonResult GetData(int userID) {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["Users"] = db.Users.Select(u => new { u.UserId, u.Name, u.AccountType }).ToList();
            data["Buys"] = db.Buys.Where(b => b.UserId == userID).Select(b => new { b.BuyId, b.UserId, b.Date, b.ExchangeWalletId, b.WalletId, b.CoinId, b.PriceEur, b.AmountBought, b.AmountInWallet }).ToList();
            data["Creations"] = db.Creations.Where(c => c.UserId == userID).Select(c => new { c.CreationId, c.UserId, c.WalletId, c.CoinId, c.Amount, c.SellWalletId, c.SellDate, c.SellPricePerShare, c.SellPriceBtc }).ToList();
            data["Trades"] = db.Trades.Where(t => t.UserId == userID).Select(t => new { t.TradeId, t.UserId, t.StoreWalletId, t.CoinId, t.Amount, t.BuyWalletId, t.BuyPricePerShare, t.BuyPriceBtc, t.BuyDate, t.SellWalletId, t.SellPricePerShare, t.SellPriceBtc, t.SellDate }).ToList();
            data["Coins"] = db.Coins.Select(c => new { c.CoinId, c.CoinMarketCapId, c.Name, c.Symbol }).ToDictionary(c => c.CoinId);
            data["Wallets"] = db.Wallets.Select(w => new { w.WalletId, w.Name }).ToDictionary(w => w.WalletId);

            return Json(data);
        }

        public JsonResult GetCoinValues() {
            Dictionary<string, Coin> coins = db.Coins.ToDictionary(c => c.CoinMarketCapId);
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = new Uri("https://api.coinmarketcap.com");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var result = client.GetStringAsync("/v1/ticker/?convert=EUR&limit=500").Result;
                Dictionary<int, CoinMarketCapResult> values = JsonConvert.DeserializeObject<List<CoinMarketCapResult>>(result).Where(c => coins.ContainsKey(c.ID)).ToDictionary(c => coins[c.ID].CoinId);
                return Json(values);
            }
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Test() {
            return View();
        }

        public IActionResult Error() {
            return View();
        }
    }
}
