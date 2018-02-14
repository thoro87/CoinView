using CoinView.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class UserSummaryDO {

        public User User;
        public decimal InvestsBuyValueEUR { get; set; }
        public decimal InvestsSellValueEUR { get; set; }
        public decimal InvestsResultValueEUR { get { return InvestsSellValueEUR - InvestsBuyValueEUR; } }
        public decimal InvestsResultPercent { get { return InvestsSellValueEUR / InvestsBuyValueEUR - 1; } }

        public decimal TradesBuyValueEUR { get; set; }
        public decimal TradesSellValueEUR { get; set; }
        public decimal TradesResultValueEUR { get { return TradesSellValueEUR - TradesBuyValueEUR; } }
        public decimal TradesResultPercent { get { return TradesSellValueEUR / TradesBuyValueEUR - 1; } }

        public decimal CreationsBuyValueEUR { get; set; }
        public decimal CreationsSellValueEUR { get; set; }
        public decimal CreationsResultValueEUR { get { return CreationsSellValueEUR - CreationsBuyValueEUR; } }

        public decimal TotalSellValueEUR { get { return InvestsSellValueEUR + TradesSellValueEUR + CreationsSellValueEUR; } }
        public decimal TotalBuyValueEUR { get { return InvestsBuyValueEUR + TradesBuyValueEUR + CreationsBuyValueEUR; } }
        public decimal TotalResultValueEUR { get { return TotalSellValueEUR - TotalBuyValueEUR; } }
        public decimal TotalResultPercent { get { return TotalSellValueEUR / TotalBuyValueEUR - 1; } }

    }
}
