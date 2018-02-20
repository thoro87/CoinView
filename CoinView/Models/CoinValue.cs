using System;
using System.Collections.Generic;

namespace CoinView.Models.Database
{
    public partial class CoinValue
    {
        public int CoinValueId { get; set; }
        public int CoinId { get; set; }
        public DateTime Date { get; set; }
        public decimal PriceBtc { get; set; }
        public decimal PriceEur { get; set; }
        public decimal PriceUsd { get; set; }
        public decimal PercentChange1h { get; set; }
        public decimal PercentChange24h { get; set; }
        public decimal PercentChange7d { get; set; }

        public Coin Coin { get; set; }
    }
}
