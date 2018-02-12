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
            data["Users"] = db.Users.Select(u => new { u.UserId, u.Name }).ToList();
            data["Buys"] = db.Buys.Where(b => b.UserId == userID).Select(b => new { b.BuyId, b.UserId, b.Date, b.Purpose, b.ExchangeWalletId, b.WalletId, b.CoinId, b.PriceEur, b.AmountBought, b.AmountInWallet }).ToList();
            data["Creations"] = db.Creations.Where(c => c.UserId == userID).Select(c => new { c.CreationId, c.UserId, c.WalletId, c.CoinId, c.Amount, c.SellWalletId, c.SellDate, c.SellPricePerShare, c.SellPriceBtc }).ToList();
            data["Trades"] = db.Trades.Where(t => t.UserId == userID).Select(t => new { t.TradeId, t.UserId, t.StoreWalletId, t.CoinId, t.Amount, t.BuyWalletId, t.BuyPricePerShare, t.BuyPriceBtc, t.BuyDate, t.SellWalletId, t.SellPricePerShare, t.SellPriceBtc, t.SellDate }).ToList();
            data["Coins"] = db.Coins.Select(c => new { c.CoinId, c.CoinMarketCapId, c.Name, c.Symbol }).ToDictionary(c => c.CoinId);
            data["Wallets"] = db.Wallets.Select(w => new { w.WalletId, w.Name }).ToDictionary(w => w.WalletId);
            data["CoinValues"] = db.CoinValues.GroupBy(c => c.CoinId).ToDictionary(g => g.Key, g => g.OrderByDescending(c => c.Date).First());

            return Json(data);
        }

        //public JsonResult GetCoinValues() {
        //    Dictionary<string, Coin> coins = db.Coins.ToDictionary(c => c.CoinMarketCapId);
        //    using (HttpClient client = new HttpClient()) {
        //        client.BaseAddress = new Uri("https://api.coinmarketcap.com");
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        var result = client.GetStringAsync("/v1/ticker/?convert=EUR&limit=500").Result;
        //        Dictionary<int, CoinMarketCapResult> values = JsonConvert.DeserializeObject<List<CoinMarketCapResult>>(result).Where(c => coins.ContainsKey(c.ID)).ToDictionary(c => coins[c.ID].CoinId);

        //        // store in db
        //        //List<CoinValue> toStore = new List<CoinValue>();
        //        //DateTime now = DateTime.Now;
        //        //foreach (CoinMarketCapResult value in values.Values) {
        //        //    toStore.Add(new CoinValue() {
        //        //        CoinId = coins[value.ID].CoinId,
        //        //        Date = now,
        //        //        PriceBtc = (decimal)value.Price_btc,
        //        //        PriceEur = (decimal)value.Price_eur,
        //        //        PriceUsd = (decimal)value.Price_usd,
        //        //        PercentChange1h = (decimal)value.Percent_change_1h,
        //        //        PercentChange24h = (decimal)value.Percent_change_24h,
        //        //        PercentChange7d = (decimal)value.Percent_change_7d
        //        //    });
        //        //}
        //        //db.CoinValues.AddRange(toStore);
        //        //db.SaveChanges();

        //        return Json(values);
        //    }
        //}

        public IActionResult Summary() {
            return View();
        }

        public IActionResult UserDetail(string id) {
            return View("UserDetail", db.Users.Single(u => u.UserId == Int32.Parse(id)));
        }

        public IActionResult Error() {
            return View();
        }
    }
}
