using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class Creation {
        public User User { get; set; }
        public Wallet Wallet { get; set; }
        public Coin Coin { get; set; }
        public Decimal Amount { get; set; }

        public Wallet SellWallet { get; set; }
        public DateTime SellDate { get; set; }
        public Decimal SellPricePerShare { get; set; }
        public Decimal SellPriceBTC { get; set; }
    }
}
