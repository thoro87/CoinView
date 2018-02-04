using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class Coin {
        public int ID { get; set; }
        public string CoinMarketCapID { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public Coin(int id, string coinMarketCapID, string name, string symbol) {
            ID = id;
            CoinMarketCapID = coinMarketCapID;
            Name = name;
            Symbol = symbol;
        }
    }
}
