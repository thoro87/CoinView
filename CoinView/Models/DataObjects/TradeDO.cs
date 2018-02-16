using CoinView.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class TradeDO {

        public Trade Trade { get; set; }
        public CoinValue CoinValue { get; set; }

        public bool IsSold { get { return Trade.SellWalletId != null; } }
        public Decimal PercentChange1h { get { return CoinValue.PercentChange1h / 100; } }
        public Decimal PercentChange24h { get { return CoinValue.PercentChange24h / 100; } }
        public Decimal PercentChange7d { get { return CoinValue.PercentChange7d / 100; } }

        public DateTime BuyDate { get { return Trade.BuyDate; } }
        public Wallet BuyWallet { get { return Trade.BuyWallet; } }
        public Decimal BuyPricePerShareBTC { get { return Trade.BuyPricePerShare; } }
        public Decimal BuyPricePerShareEUR { get { return Trade.BuyPricePerShare * BuyPriceBTC; } }
        public Decimal BuyPriceBTC { get { return Trade.BuyPriceBtc; } }
        public Decimal BuyValueBTC { get { return Trade.Amount * Trade.BuyPricePerShare; } }
        public Decimal BuyValueEUR { get { return Trade.Amount * Trade.BuyPricePerShare * BuyPriceBTC; } }

        public DateTime? SellDate { get { return Trade.SellDate; } }
        public Wallet SellWallet { get { return Trade.SellWallet; } }
        public Decimal SellPricePerShareBTC { get { return IsSold ? Trade.SellPricePerShare.Value : CoinValue.PriceBtc; } }
        public Decimal SellPricePerShareEUR { get { return IsSold ? Trade.SellPricePerShare.Value * Trade.SellPriceBtc.Value : CoinValue.PriceEur; } }
        public Decimal? SellPriceBTC { get { return Trade.SellPriceBtc; } }
        public Decimal SellValueBTC { get { return Trade.Amount * SellPricePerShareBTC; } }
        public Decimal SellValueEUR { get { return Trade.Amount * SellPricePerShareEUR; } }

        public Decimal ProfitValueEUR { get { return SellValueEUR - BuyValueEUR; } }
        public Decimal ProfitValueEURPercent { get { return SellValueEUR / BuyValueEUR - 1; } }

        public TradeDO(Trade trade, CoinValue coinValue) {
            Trade = trade;
            CoinValue = coinValue;
        }
    }
}
