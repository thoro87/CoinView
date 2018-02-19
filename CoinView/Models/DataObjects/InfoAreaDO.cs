using CoinView.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class InfoAreaDO {

        public CoinValue LatestBitcoinValue { get; set; }

        public String BitcoinPriceUSD { get { return LatestBitcoinValue.PriceUsd.ToString("C", CultureInfo.CreateSpecificCulture("en-US")); } }
        public String BitcoinPriceEUR { get { return LatestBitcoinValue.PriceEur.ToString("C"); } }
        public Decimal BitcoinChangePercent1h { get { return LatestBitcoinValue.PercentChange1h / 100; } }
        public Decimal BitcoinChangePercent24h { get { return LatestBitcoinValue.PercentChange24h / 100; } }
        public Decimal BitcoinChangePercent7d { get { return LatestBitcoinValue.PercentChange7d / 100; } }
        public DateTime Timestamp { get { return LatestBitcoinValue.Date; } }

        public InfoAreaDO(CoinValue latestBitcoinValue) {
            LatestBitcoinValue = latestBitcoinValue;
        }
    }
}
