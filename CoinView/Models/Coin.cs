using System;
using System.Collections.Generic;

namespace CoinView.Models.Database
{
    public partial class Coin
    {
        public Coin()
        {
            Buys = new HashSet<Buy>();
            CoinValues = new HashSet<CoinValue>();
            Creations = new HashSet<Creation>();
            Trades = new HashSet<Trade>();
        }

        public int CoinId { get; set; }
        public string CoinMarketCapId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public ICollection<Buy> Buys { get; set; }
        public ICollection<CoinValue> CoinValues { get; set; }
        public ICollection<Creation> Creations { get; set; }
        public ICollection<Trade> Trades { get; set; }
    }
}
