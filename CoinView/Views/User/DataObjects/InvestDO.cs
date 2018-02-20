using CoinView.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class InvestDO {

        public Buy Buy { get; set; }
        public CoinValue CoinValue { get; set; }

        public DateTime Date { get { return Buy.Date; } }
        public Wallet ExchangeWallet { get { return Buy.ExchangeWallet; } }
        public Wallet Wallet { get { return Buy.Wallet; } }
        public Decimal PriceEUR { get { return Buy.PriceEur; } }
        public Decimal AmountBought { get { return Buy.AmountBought; } }
        public Decimal BuyValueEUR { get { return Buy.AmountBought * PriceEUR; } }
        public Decimal SellValueEUR { get { return Buy.AmountInWallet * CoinValue.PriceEur; } }

        public Decimal ProfitValueEUR { get { return SellValueEUR - BuyValueEUR; } }
        public Decimal ProfitValueEURPercent { get { return SellValueEUR / BuyValueEUR - 1; } }

        public InvestDO(Buy buy, CoinValue coinValue) {
            Buy = buy;
            CoinValue = coinValue;
        }
    }
}
