using System;
using System.Collections.Generic;

namespace CoinView.Models.Database
{
    public partial class Snapshot
    {
        public int SnapshotId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal InvestsBuyValueEur { get; set; }
        public decimal InvestsSellValueEur { get; set; }
        public decimal TradesBuyValueEur { get; set; }
        public decimal TradesSellValueEur { get; set; }
        public decimal CreationsBuyValueEur { get; set; }
        public decimal CreationsSellValueEur { get; set; }

        public User User { get; set; }
    }
}
