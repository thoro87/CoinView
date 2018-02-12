using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoinView.Models.Database;

namespace CoinView.Controllers {
    public class SummaryController : Controller {

        private readonly CoinViewContext db;

        public SummaryController(CoinViewContext _context) {
            db = _context;
        }

        public JsonResult GetData() {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data["CoinValues"] = db.CoinValues.GroupBy(c => c.CoinId).ToDictionary(g => g.Key, g => g.OrderByDescending(c => c.Date).First());
            data["Buys"] = db.Buys.Where(b => b.UserId == 1).Select(b => new { b.BuyId, b.UserId, b.Date, b.Purpose, b.ExchangeWalletId, b.WalletId, b.CoinId, b.PriceEur, b.AmountBought, b.AmountInWallet }).ToList();
            data["OpenTrades"] = db.Trades.Where(t => t.UserId == 1 && t.SellWallet == null).Select(t => new { t.TradeId, t.UserId, t.StoreWalletId, t.CoinId, t.Amount, t.BuyWalletId, t.BuyPricePerShare, t.BuyPriceBtc, t.BuyDate, t.SellWalletId, t.SellPricePerShare, t.SellPriceBtc, t.SellDate }).ToList();
            data["OpenCreations"] = db.Creations.Where(c => c.UserId == 1 && c.SellWallet == null).Select(c => new { c.CreationId, c.UserId, c.WalletId, c.CoinId, c.Amount, c.SellWalletId, c.SellDate, c.SellPricePerShare, c.SellPriceBtc }).ToList();


            //data["Users"] = db.Users.Select(u => new { u.UserId, u.Name }).ToList();
            //data["Coins"] = db.Coins.Select(c => new { c.CoinId, c.CoinMarketCapId, c.Name, c.Symbol }).ToDictionary(c => c.CoinId);
            //data["Wallets"] = db.Wallets.Select(w => new { w.WalletId, w.Name }).ToDictionary(w => w.WalletId);

            return Json(data);
        }

        public IActionResult Summary() {
            return View();
        }
    }
}