using CoinView.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class SnapshotDO {

        public Snapshot Snapshot;

        public DateTime Date { get { return Snapshot.Date; } }
        public Decimal InvestsBuyValueEUR { get { return Snapshot.InvestsBuyValueEur; } }
        public Decimal InvestsSellValueEUR { get { return Snapshot.InvestsSellValueEur; } }
        public Decimal TradesBuyValueEUR { get { return Snapshot.TradesBuyValueEur; } }
        public Decimal TradesSellValueEUR { get { return Snapshot.TradesSellValueEur; } }
        public Decimal CreationsBuyValueEUR { get { return Snapshot.CreationsBuyValueEur; } }
        public Decimal CreationsSellValueEUR { get { return Snapshot.CreationsSellValueEur; } }

        public Decimal TotalBuyValueEUR { get { return InvestsBuyValueEUR + TradesBuyValueEUR + CreationsBuyValueEUR; } }
        public Decimal TotalSellValueEUR { get { return InvestsSellValueEUR + TradesSellValueEUR + CreationsSellValueEUR; } }

        public SnapshotDO(Snapshot snapshot) {
            Snapshot = snapshot;
        }

    }
}
