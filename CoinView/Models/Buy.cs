using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class Buy {
        public User User { get; set; }
        public DateTime Date { get; set; }
        public Wallet Exchange { get; set; }
        public Wallet Wallet { get; set; }
        public Coin Coin { get; set; }
        public Decimal PriceEUR { get; set; }
        public Decimal Amount { get; set; }
        public Decimal AmountAfterFee { get; set; }

        public Buy() {
            Coin = CoinFactory.Bitcoin;
        }
    }
}
