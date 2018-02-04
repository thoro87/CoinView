using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public static class WalletFactory {

        public static Wallet BitcoinDe;
        public static Wallet Electrum;
        public static Wallet Poloniex;
        public static Wallet GridcoinWallet;
        public static Wallet Bittrex;
        public static Wallet Bitfinex;
        public static Wallet IotaWallet;
        public static Wallet Exodus;
        public static Wallet Steemit;
        public static Wallet Kraken;
        public static Wallet Binance;

        static WalletFactory() {
            BitcoinDe = new Wallet(1, "Bitcoin.de");
            Electrum = new Wallet(2, "Electrum");
            Poloniex = new Wallet(3, "Poloniex");
            GridcoinWallet = new Wallet(4, "Gridcoin Wallet");
            Bittrex = new Wallet(5, "Bittrex");
            Bitfinex = new Wallet(6, "Bitfinex");
            IotaWallet = new Wallet(7, "IOTA Wallet");
            Exodus = new Wallet(8, "Exodus");
            Steemit = new Wallet(9, "Steemit");
            Kraken = new Wallet(10, "Kraken");
            Binance = new Wallet(11, "Binance");
        }

        public static List<Wallet> All() {
            return new List<Wallet>() {
                BitcoinDe, Electrum, Poloniex, GridcoinWallet, Bittrex, Bitfinex, IotaWallet, Exodus, Steemit, Kraken, Binance
            };
        }

    }
}
