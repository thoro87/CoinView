﻿using System;
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

                StoreSnapshot(toStore.ToDictionary(t => t.CoinId));
            }

            return View("Summary", GetModel());
        }

        private void StoreSnapshot(Dictionary<int, CoinValue> coinValues) {
            DateTime date = coinValues[1].Date;

            List<Snapshot> newSnapshots = new List<Snapshot>();
            foreach (User user in db.Users) {
                Snapshot newSnapshot = new Snapshot() {
                    UserId = user.UserId,
                    Date = date,
                    InvestsBuyValueEur = db.Buys.Where(b => b.UserId == user.UserId && b.Purpose == "Invest").Select(b => b.AmountBought * b.PriceEur).Sum(),
                    InvestsSellValueEur = db.Buys.Where(b => b.UserId == user.UserId && b.Purpose == "Invest").Select(b => b.AmountInWallet * coinValues[1].PriceEur).Sum(),
                    TradesBuyValueEur = db.Buys.Where(b => b.UserId == user.UserId && b.Purpose == "Trade").Select(b => b.AmountBought * b.PriceEur).Sum(),
                    TradesSellValueEur = db.Trades.Where(t => t.UserId == user.UserId && (t.SellDate == null || t.SellDate > DateTime.Now)).Select(t => t.Amount * coinValues[t.CoinId].PriceEur).Sum(),
                    CreationsBuyValueEur = 0,
                    CreationsSellValueEur = db.Creations.Where(c => c.UserId == user.UserId && (c.SellDate == null || c.SellDate > DateTime.Now)).Select(c => c.Amount * coinValues[c.CoinId].PriceEur).Sum()
                };
                newSnapshots.Add(newSnapshot);
            }

            db.Snapshots.AddRange(newSnapshots);
            db.SaveChanges();
        }

        private SummaryViewModel GetModel() {
            return new SummaryViewModel() {
                UserSummaries = GetUserSummaries(),
                CombinedSummary = GetCombinedSummary(),
                InfoArea = new InfoAreaDO(db.CoinValues.Where(c => c.CoinId == 1).OrderByDescending(c => c.Date).First())
            };
        }

        private List<SummaryDO> GetUserSummaries() {
            List<SummaryDO> userSummaries = new List<SummaryDO>();
            List<User> users = db.Users.ToList();
            foreach (User user in users) {
                userSummaries.Add(GetSummary(user.Name, new List<int>() { user.UserId }));
            }
            return userSummaries;
        }

        private SummaryDO GetCombinedSummary() {
            return GetSummary("Combined", db.Users.Select(u => u.UserId).ToList());
        }

        private SummaryDO GetSummary(string name, List<int> userIDsToInclude) {
            Dictionary<int, CoinValue> coinValues = db.CoinValues.GroupBy(c => c.CoinId).ToDictionary(g => g.Key, g => g.OrderByDescending(c => c.Date).First());
            List<Buy> buys = db.Buys.Where(b => userIDsToInclude.Contains(b.UserId)).ToList();
            List<Trade> trades = db.Trades.Where(t => userIDsToInclude.Contains(t.UserId) && t.SellWallet == null).ToList();
            List<Creation> creations = db.Creations.Where(c => userIDsToInclude.Contains(c.UserId) && c.SellWallet == null).ToList();
            List<Snapshot> snapshots = db.Snapshots.Where(s => userIDsToInclude.Contains(s.UserId)).ToList();

            return new SummaryDO(name, coinValues, buys, trades, creations, snapshots);
        }



    }
}