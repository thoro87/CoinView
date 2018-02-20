using System;
using System.Collections.Generic;

namespace CoinView.Models.Database
{
    public partial class Creation
    {
        public int CreationId { get; set; }
        public int UserId { get; set; }
        public int WalletId { get; set; }
        public int CoinId { get; set; }
        public decimal Amount { get; set; }
        public int? SellWalletId { get; set; }
        public DateTime? SellDate { get; set; }
        public decimal? SellPricePerShare { get; set; }
        public decimal? SellPriceBtc { get; set; }

        public Coin Coin { get; set; }
        public Wallet SellWallet { get; set; }
        public User User { get; set; }
        public Wallet Wallet { get; set; }
    }
}
