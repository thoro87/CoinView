using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class Investment {
        public User User { get; set; }
        public DateTime Date { get; set; }
        public Coin Coin { get; set; }
        public Wallet Exchange { get; set; }
        public Wallet Wallet { get; set; }
        public Decimal PriceEUR { get; set; }
        public Decimal AmountBought { get; set; }
        public Decimal AmountInWallet { get; set; }

        public Investment() {
            Coin = CoinFactory.Bitcoin;
        }
    }
}
