using CoinView.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class UserViewModel {

        public User User { get; set; }
        
        public List<InvestDO> Invests { get; set; }
        public Decimal InvestsAmountBoughtSum { get { return Invests.Select(i => i.AmountBought).Sum(); } }
        public Decimal InvestsBuyValueEURSum { get { return Invests.Select(i => i.BuyValueEUR).Sum(); } }
        public Decimal InvestsSellValueEURSum { get { return Invests.Select(i => i.SellValueEUR).Sum(); } }
        public Decimal InvestsProfitValueEURSum { get { return Invests.Select(i => i.ProfitValueEUR).Sum(); } }
        public Decimal InvestsProfitValueEURSumPercent { get { return InvestsSellValueEURSum / InvestsBuyValueEURSum - 1; } }

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

        public List<CreationDO> OpenCreations { get; set; }
        public Decimal OpenCreationsSellValueBTCSum { get { return OpenCreations.Select(c => c.SellValueBTC).Sum(); } }
        public Decimal OpenCreationsSellValueEURSum { get { return OpenCreations.Select(c => c.SellValueEUR).Sum(); } }

        public List<CreationDO> ClosedCreations { get; set; }
        public Decimal ClosedCreationsSellValueBTCSum { get { return ClosedCreations.Select(c => c.SellValueBTC).Sum(); } }
        public Decimal ClosedCreationsSellValueEURSum { get { return ClosedCreations.Select(c => c.SellValueEUR).Sum(); } }
    }
}
