using CoinView.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class UserViewModel {

        public User User { get; set; }
        public List<TradeDO> OpenTrades { get; set; }
        public List<TradeDO> ClosedTrades { get; set; }
    }
}
