using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class Wallet {
        public int ID { get; set; }
        public string Name { get; set; }
        public Wallet(int id, string name) {
            ID = id;
            Name = name;
        }
    }
}
