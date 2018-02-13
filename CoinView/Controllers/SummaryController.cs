using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoinView.Models.Database;
using CoinView.Models;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CoinView.Controllers {
    public class SummaryController : Controller {

        private readonly CoinViewContext db;

        public SummaryController(CoinViewContext _context) {
            db = _context;
        }

        public IActionResult GetData() {
            return View("Summary", GetModel());
        }

        public IActionResult Update() {
            Dictionary<string, Coin> coins = db.Coins.ToDictionary(c => c.CoinMarketCapId);
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = new Uri("https://api.coinmarketcap.com");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var result = client.GetStringAsync("/v1/ticker/?convert=EUR&limit=500").Result;
                Dictionary<int, CoinMarketCapResult> values = JsonConvert.DeserializeObject<List<CoinMarketCapResult>>(result).Where(c => coins.ContainsKey(c.ID)).ToDictionary(c => coins[c.ID].CoinId);

                List<CoinValue> toStore = new List<CoinValue>();
                DateTime now = DateTime.Now;
                foreach (CoinMarketCapResult value in values.Values) {
                    toStore.Add(new CoinValue() {
                        CoinId = coins[value.ID].CoinId,
                        Date = now,
                        PriceBtc = (decimal)value.Price_btc,
                        PriceEur = (decimal)value.Price_eur,
                        PriceUsd = (decimal)value.Price_usd,
                        PercentChange1h = (decimal)value.Percent_change_1h,
                        PercentChange24h = (decimal)value.Percent_change_24h,
                        PercentChange7d = (decimal)value.Percent_change_7d
                    });
                }
                db.CoinValues.AddRange(toStore);
                db.SaveChanges();
            }

            return View("Summary", GetModel());
        }

        private dynamic GetModel() {
            dynamic model = new ExpandoObject();
            model.SummaryJanis = GetSummaryForUser(1);
            model.SummaryLena = GetSummaryForUser(3);
            return model;
        }

        private UserSummaryDO GetSummaryForUser(int userID) {

            List<Buy> buys = db.Buys.Where(b => b.UserId == userID).ToList();
            Dictionary<int, CoinValue> coinValues = db.CoinValues.GroupBy(c => c.CoinId).ToDictionary(g => g.Key, g => g.OrderByDescending(c => c.Date).First());
            List<Trade> trades = db.Trades.Where(t => t.UserId == userID && t.SellWallet == null).ToList();
            List<Creation> creations = db.Creations.Where(c => c.UserId == userID && c.SellWallet == null).ToList();

            return new UserSummaryDO() {
                UserID = userID,
                InvestsBuyValueEUR = buys.Where(b => b.Purpose == "Invest").Select(b => b.AmountBought * b.PriceEur).Sum(),
                InvestsSellValueEUR = buys.Where(b => b.Purpose == "Invest").Select(b => b.AmountInWallet * coinValues[1].PriceEur).Sum(),
                TradesBuyValueEUR = buys.Where(b => b.Purpose == "Trade").Select(b => b.AmountBought * b.PriceEur).Sum(),
                TradesSellValueEUR = trades.Select(t => t.Amount * coinValues[t.CoinId].PriceEur).Sum(),
                CreationsBuyValueEUR = 0,
                CreationsSellValueEUR = creations.Select(c => c.Amount * coinValues[c.CoinId].PriceEur).Sum()
            };
        }



    }
}