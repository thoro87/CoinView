using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoinView.Models.Database;
using CoinView.Models;
using Microsoft.EntityFrameworkCore;

namespace CoinView.Controllers {
    public class UserController : Controller {

        private readonly CoinViewContext db;

        public UserController(CoinViewContext _context) {
            db = _context;
        }

        public IActionResult GetData(string id) {
            int userID = Int32.Parse(id);
            Dictionary<int, CoinValue> coinValues = db.CoinValues.GroupBy(c => c.CoinId).ToDictionary(g => g.Key, g => g.OrderByDescending(c => c.Date).First());

            UserViewModel model = new UserViewModel() {
                User = db.Users.Single(u => u.UserId == userID),
                Invests = db.Buys.Where(b => b.UserId == userID && b.Purpose == "Invest").Include(b => b.Wallet).Include(b => b.ExchangeWallet).Select(b => new InvestDO(b, coinValues[1])).OrderBy(i => i.Date).ToList(),
                OpenTrades = db.Trades.Where(t => t.UserId == userID && t.SellWallet == null).Include(t => t.Coin).Include(t => t.StoreWallet).Include(t => t.BuyWallet).Include(t => t.SellWallet).Select(t => new TradeDO(t, coinValues[t.CoinId])).OrderByDescending(t => t.ProfitValueEUR).ToList(),
                ClosedTrades = db.Trades.Where(t => t.UserId == userID && t.SellWallet != null).Include(t => t.Coin).Include(t => t.StoreWallet).Include(t => t.BuyWallet).Include(t => t.SellWallet).Select(t => new TradeDO(t, coinValues[t.CoinId])).OrderByDescending(t => t.ProfitValueEUR).ToList(),
                OpenCreations = db.Creations.Where(c => c.UserId == userID && c.SellWallet == null).Include(c => c.Coin).Include(c => c.Wallet).Select(c => new CreationDO(c, coinValues[c.CoinId])).OrderByDescending(c => c.SellValueEUR).ToList(),
                ClosedCreations = db.Creations.Where(c => c.UserId == userID && c.SellWallet != null).Include(c => c.Coin).Include(c => c.Wallet).Select(c => new CreationDO(c, coinValues[c.CoinId])).OrderByDescending(c => c.SellValueEUR).ToList(),
                InfoArea = new InfoAreaDO(coinValues[1])
            };

            return View("User", model);
        }


    }
}