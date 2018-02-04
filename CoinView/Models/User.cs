using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {


    public class User {
        public enum AccountTypes {
            Trading,
            Investment
        };

        public int ID { get; set; }
        public string Name { get; set; }
        public AccountTypes AccountType { get; set; }
    }
}
