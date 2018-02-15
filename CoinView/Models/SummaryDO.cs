using CoinView.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class SummaryDO {

        public string Name;

        public decimal InvestsBuyValueEUR { get; set; }
        public decimal InvestsSellValueEUR { get; set; }
        public decimal InvestsResultValueEUR { get { return InvestsSellValueEUR - InvestsBuyValueEUR; } }
        public decimal InvestsResultValueEURPercent { get { return InvestsSellValueEUR / InvestsBuyValueEUR - 1; } }
        public decimal InvestsBuyValueBTC { get; set; }
        public decimal InvestsSellValueBTC { get; set; }
        public decimal InvestsResultValueBTC { get { return InvestsSellValueBTC - InvestsBuyValueBTC; } }
        public decimal InvestsResultValueBTCPercent { get { return InvestsSellValueBTC / InvestsBuyValueBTC - 1; } }

        public decimal TradesBuyValueEUR { get; set; }
        public decimal TradesSellValueEUR { get; set; }
        public decimal TradesResultValueEUR { get { return TradesSellValueEUR - TradesBuyValueEUR; } }
        public decimal TradesResultValueEURPercent { get { return TradesSellValueEUR / TradesBuyValueEUR - 1; } }
        public decimal TradesBuyValueBTC { get; set; }
        public decimal TradesSellValueBTC { get; set; }
        public decimal TradesResultValueBTC { get { return TradesSellValueBTC - TradesBuyValueBTC; } }
        public decimal TradesResultValueBTCPercent { get { return TradesSellValueBTC / TradesBuyValueBTC - 1; } }

        public decimal CreationsBuyValueEUR { get; set; }
        public decimal CreationsSellValueEUR { get; set; }
        public decimal CreationsResultValueEUR { get { return CreationsSellValueEUR - CreationsBuyValueEUR; } }
        public decimal CreationsBuyValueBTC { get; set; }
        public decimal CreationsSellValueBTC { get; set; }
        public decimal CreationsResultValueBTC { get { return CreationsSellValueBTC - CreationsBuyValueBTC; } }

        public decimal TotalSellValueEUR { get { return InvestsSellValueEUR + TradesSellValueEUR + CreationsSellValueEUR; } }
        public decimal TotalBuyValueEUR { get { return InvestsBuyValueEUR + TradesBuyValueEUR + CreationsBuyValueEUR; } }
        public decimal TotalResultValueEUR { get { return TotalSellValueEUR - TotalBuyValueEUR; } }
        public decimal TotalResultValueEURPercent { get { return TotalSellValueEUR / TotalBuyValueEUR - 1; } }
        public decimal TotalSellValueBTC { get { return InvestsSellValueBTC + TradesSellValueBTC + CreationsSellValueBTC; } }
        public decimal TotalBuyValueBTC { get { return InvestsBuyValueBTC + TradesBuyValueBTC + CreationsBuyValueBTC; } }
        public decimal TotalResultValueBTC { get { return TotalSellValueBTC - TotalBuyValueBTC; } }
        public decimal TotalResultValueBTCPercent { get { return TotalSellValueBTC / TotalBuyValueBTC - 1; } }

    }
}
