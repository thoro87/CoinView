using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class UserSummaryDO {

        public int UserID;
        public decimal InvestsBuyValueEUR { get; set; }
        public decimal InvestsSellValueEUR { get; set; }
        public decimal InvestsResultValueEUR { get { return InvestsSellValueEUR - InvestsBuyValueEUR; } }
        public decimal TradesBuyValueEUR { get; set; }
        public decimal TradesSellValueEUR { get; set; }
        public decimal TradesResultValueEUR { get { return TradesSellValueEUR - TradesBuyValueEUR; } }
        public decimal CreationsBuyValueEUR { get; set; }
        public decimal CreationsSellValueEUR { get; set; }
        public decimal CreationsResultValueEUR { get { return CreationsSellValueEUR - CreationsBuyValueEUR; } }

        public decimal SellTotalEUR { get { return InvestsSellValueEUR + TradesSellValueEUR + CreationsSellValueEUR; } }
        public decimal BuyTotalEUR { get { return InvestsBuyValueEUR + TradesBuyValueEUR + CreationsBuyValueEUR; } }
        public decimal ResultTotalEUR { get { return SellTotalEUR - BuyTotalEUR; } }
    }
}
