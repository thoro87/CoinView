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
        public Decimal OpenTradesBuyValueEURSum { get { return OpenTrades.Select(t => t.BuyValueEUR).Sum(); } }
        public Decimal OpenTradesSellValueEURSum { get { return OpenTrades.Select(t => t.SellValueEUR).Sum(); } }
        public Decimal OpenTradesProfitValueEURSum { get { return OpenTrades.Select(t => t.ProfitValueEUR).Sum(); } }
        public Decimal OpenTradesProfitValueEURSumPercent { get { return OpenTradesSellValueEURSum / OpenTradesBuyValueEURSum - 1; } }

        public List<TradeDO> ClosedTrades { get; set; }
        public Decimal ClosedTradesBuyValueEURSum { get { return ClosedTrades.Select(t => t.BuyValueEUR).Sum(); } }
        public Decimal ClosedTradesSellValueEURSum { get { return ClosedTrades.Select(t => t.SellValueEUR).Sum(); } }
        public Decimal ClosedTradesProfitValueEURSum { get { return ClosedTrades.Select(t => t.ProfitValueEUR).Sum(); } }
        public Decimal ClosedTradesProfitValueEURSumPercent { get { return ClosedTradesSellValueEURSum / ClosedTradesBuyValueEURSum - 1; } }
    }
}
