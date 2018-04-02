using CoinView.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class SummaryDO {

        public string Name;
        public Dictionary<int, CoinValue> CoinValues;
        public List<Buy> Buys;
        public List<Trade> Trades;
        public List<Creation> Creations;
        public List<SnapshotDO> Snapshots;

        public decimal InvestsBuyValueEUR { get { return Buys.Where(b => b.Purpose == "Invest").Select(b => b.AmountBought * b.PriceEur).Sum(); } }
        public decimal InvestsSellValueEUR { get { return Buys.Where(b => b.Purpose == "Invest").Select(b => b.AmountInWallet * CoinValues[1].PriceEur).Sum(); } }
        public decimal InvestsResultValueEUR { get { return InvestsSellValueEUR - InvestsBuyValueEUR; } }
        public decimal InvestsResultValueEURPercent { get { return InvestsSellValueEUR / InvestsBuyValueEUR - 1; } }
        public decimal InvestsBuyValueBTC { get { return Buys.Where(b => b.Purpose == "Invest").Select(b => b.AmountBought).Sum(); } }
        public decimal InvestsSellValueBTC { get { return Buys.Where(b => b.Purpose == "Invest").Select(b => b.AmountInWallet).Sum(); } }
        public decimal InvestsResultValueBTC { get { return InvestsSellValueBTC - InvestsBuyValueBTC; } }
        public decimal InvestsResultValueBTCPercent { get { return InvestsSellValueBTC / InvestsBuyValueBTC - 1; } }

        public decimal TradesBuyValueEUR { get { return Buys.Where(b => b.Purpose == "Trade").Select(b => b.AmountBought * b.PriceEur).Sum(); } }
        public decimal TradesSellValueEUR { get { return Trades.Select(t => t.Amount * CoinValues[t.CoinId].PriceEur).Sum(); } }
        public decimal TradesResultValueEUR { get { return TradesSellValueEUR - TradesBuyValueEUR; } }
        public decimal TradesResultValueEURPercent { get { return TradesSellValueEUR / TradesBuyValueEUR - 1; } }
        public decimal TradesBuyValueBTC { get { return Buys.Where(b => b.Purpose == "Trade").Select(b => b.AmountBought).Sum(); } }
        public decimal TradesSellValueBTC { get { return Trades.Select(t => t.Amount * CoinValues[t.CoinId].PriceBtc).Sum(); } }
        public decimal TradesResultValueBTC { get { return TradesSellValueBTC - TradesBuyValueBTC; } }
        public decimal TradesResultValueBTCPercent { get { return TradesSellValueBTC / TradesBuyValueBTC - 1; } }

        public decimal CreationsBuyValueEUR { get { return 0; } }
        public decimal CreationsSellValueEUR { get { return Creations.Select(c => c.Amount * CoinValues[c.CoinId].PriceEur).Sum(); } }
        public decimal CreationsResultValueEUR { get { return CreationsSellValueEUR - CreationsBuyValueEUR; } }
        public decimal CreationsBuyValueBTC { get { return 0; } }
        public decimal CreationsSellValueBTC { get { return Creations.Select(c => c.Amount * CoinValues[c.CoinId].PriceBtc).Sum(); } }
        public decimal CreationsResultValueBTC { get { return CreationsSellValueBTC - CreationsBuyValueBTC; } }

        public decimal TotalSellValueEUR { get { return InvestsSellValueEUR + TradesSellValueEUR + CreationsSellValueEUR; } }
        public decimal TotalBuyValueEUR { get { return InvestsBuyValueEUR + TradesBuyValueEUR + CreationsBuyValueEUR; } }
        public decimal TotalResultValueEUR { get { return TotalSellValueEUR - TotalBuyValueEUR; } }
        public decimal TotalResultValueEURPercent { get { return TotalSellValueEUR / TotalBuyValueEUR - 1; } }
        public decimal TotalSellValueBTC { get { return InvestsSellValueBTC + TradesSellValueBTC + CreationsSellValueBTC; } }
        public decimal TotalBuyValueBTC { get { return InvestsBuyValueBTC + TradesBuyValueBTC + CreationsBuyValueBTC; } }
        public decimal TotalResultValueBTC { get { return TotalSellValueBTC - TotalBuyValueBTC; } }
        public decimal TotalResultValueBTCPercent { get { return TotalSellValueBTC / TotalBuyValueBTC - 1; } }

        public List<string> ChartDates { get { return Snapshots.Select(s => s.Date).Distinct().OrderBy(d => d).Select(d => d.ToShortDateString()).ToList(); } }
        public List<Decimal> ChartTotalBuyValuesEUR { get { return Snapshots.OrderBy(s => s.Date).GroupBy(s => s.Date).Select(g => g.Select(x => x.TotalBuyValueEUR).Sum()).ToList(); } }
        public List<Decimal> ChartTotalSellValuesEUR { get { return Snapshots.OrderBy(s => s.Date).GroupBy(s => s.Date).Select(g => g.Select(x => x.TotalSellValueEUR).Sum()).ToList(); } }
        public List<Decimal> ChartInvestsSellValuesEUR { get { return Snapshots.OrderBy(s => s.Date).GroupBy(s => s.Date).Select(g => g.Select(x => x.InvestsSellValueEUR).Sum()).ToList(); } }
        public List<Decimal> ChartTradesSellValuesEUR { get { return Snapshots.OrderBy(s => s.Date).GroupBy(s => s.Date).Select(g => g.Select(x => x.TradesSellValueEUR).Sum()).ToList(); } }
        public List<Decimal> ChartHouseStartEUR { get { return ChartDates.Select(c => 50000m).ToList(); } }

        public SummaryDO(string name, Dictionary<int, CoinValue> coinValues, List<Buy> buys, List<Trade> trades, List<Creation> creations, List<Snapshot> snapshots) {
            Name = name;
            CoinValues = coinValues;
            Buys = buys;
            Trades = trades;
            Creations = creations;
            Snapshots = snapshots.Select(s => new SnapshotDO(s)).ToList();
        }

    }
}
