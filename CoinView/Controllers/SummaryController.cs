using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoinView.Models.Database;
using CoinView.Models;

namespace CoinView.Controllers {
    public class SummaryController : Controller {

        private readonly CoinViewContext db;

        public SummaryController(CoinViewContext _context) {
            db = _context;
        }

        public IActionResult Summary() {

            UserSummaryDO userSummary = GetSummaryForUser(1);

            return View(userSummary);
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