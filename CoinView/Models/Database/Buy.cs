using System;
using System.Collections.Generic;

namespace CoinView.Models.Database
{
    public partial class Buy
    {
        public int BuyId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Purpose { get; set; }
        public int ExchangeWalletId { get; set; }
        public int WalletId { get; set; }
        public int CoinId { get; set; }
        public decimal PriceEur { get; set; }
        public decimal AmountBought { get; set; }
        public decimal AmountInWallet { get; set; }

        public Coin Coin { get; set; }
        public Wallet ExchangeWallet { get; set; }
        public User User { get; set; }
        public Wallet Wallet { get; set; }
    }
}
