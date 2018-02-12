using System;
using System.Collections.Generic;

namespace CoinView.Models.Database
{
    public partial class Wallet
    {
        public Wallet()
        {
            BuyExchangeWallets = new HashSet<Buy>();
            BuyWallets = new HashSet<Buy>();
            CreationSellWallets = new HashSet<Creation>();
            CreationWallets = new HashSet<Creation>();
            TradeBuyWallets = new HashSet<Trade>();
            TradeSellWallets = new HashSet<Trade>();
            TradeStoreWallets = new HashSet<Trade>();
        }

        public int WalletId { get; set; }
        public string Name { get; set; }

        public ICollection<Buy> BuyExchangeWallets { get; set; }
        public ICollection<Buy> BuyWallets { get; set; }
        public ICollection<Creation> CreationSellWallets { get; set; }
        public ICollection<Creation> CreationWallets { get; set; }
        public ICollection<Trade> TradeBuyWallets { get; set; }
        public ICollection<Trade> TradeSellWallets { get; set; }
        public ICollection<Trade> TradeStoreWallets { get; set; }
    }
}
