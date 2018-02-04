using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class Trade {
        public User User { get; set; }
        public Wallet StoreWallet { get; set; }
        public Coin Coin { get; set; }
        public decimal Amount { get; set; }

        public Wallet BuyWallet { get; set; }
        public decimal BuyPricePerShare { get; set; }
        public decimal BuyPriceBTC { get; set; }
        public DateTime BuyDate { get; set; }

        public Wallet SellWallet { get; set; }
        public decimal? SellPricePerShare { get; set; }
        public decimal? SellPriceBTC { get; set; }
        public DateTime? SellDate { get; set; }
    }
}
