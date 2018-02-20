using CoinView.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class CreationDO {

        public Creation Creation { get; set; }
        public CoinValue CoinValue { get; set; }

        public Wallet Wallet { get { return Creation.Wallet; } }
        public Coin Coin { get { return Coin; } }
        public Decimal Amount { get { return Creation.Amount; } }

        public Decimal PercentChange1h { get { return CoinValue.PercentChange1h / 100; } }
        public Decimal PercentChange24h { get { return CoinValue.PercentChange24h / 100; } }
        public Decimal PercentChange7d { get { return CoinValue.PercentChange7d / 100; } }

        public bool IsSold { get { return Creation.SellWalletId != null; } }
        public DateTime? SellDate { get { return Creation.SellDate; } }
        public Wallet SellWallet { get { return Creation.SellWallet; } }
        public Decimal SellPricePerShareBTC { get { return IsSold ? Creation.SellPricePerShare.Value : CoinValue.PriceBtc; } }
        public Decimal SellPricePerShareEUR { get { return IsSold ? Creation.SellPricePerShare.Value * Creation.SellPriceBtc.Value : CoinValue.PriceEur; } }
        public Decimal? SellPriceBTC { get { return Creation.SellPriceBtc; } }
        public Decimal SellValueBTC { get { return Creation.Amount * SellPricePerShareBTC; } }
        public Decimal SellValueEUR { get { return Creation.Amount * SellPricePerShareEUR; } }

        public CreationDO(Creation creation, CoinValue coinValue) {
            Creation = creation;
            CoinValue = coinValue;
        }
    }
}
