using System;
using System.Collections.Generic;

namespace CoinView.Models.Database
{
    public partial class Trade
    {
        public int TradeId { get; set; }
        public int UserId { get; set; }
        public int StoreWalletId { get; set; }
        public int CoinId { get; set; }
        public decimal Amount { get; set; }
        public int BuyWalletId { get; set; }
        public decimal BuyPricePerShare { get; set; }
        public decimal BuyPriceBtc { get; set; }
        public DateTime BuyDate { get; set; }
        public int? SellWalletId { get; set; }
        public decimal? SellPricePerShare { get; set; }
        public decimal? SellPriceBtc { get; set; }
        public DateTime? SellDate { get; set; }

        public Wallet BuyWallet { get; set; }
        public Coin Coin { get; set; }
        public Wallet SellWallet { get; set; }
        public Wallet StoreWallet { get; set; }
        public User User { get; set; }
    }
}
