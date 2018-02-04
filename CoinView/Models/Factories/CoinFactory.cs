using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public static class CoinFactory {

        public static Coin Bitcoin;
        public static Coin Litecoin;
        public static Coin Ethereum;
        public static Coin Gridcoin;
        public static Coin Iota;
        public static Coin OmiseGo;
        public static Coin Bitshares;
        public static Coin Neo;
        public static Coin AdEx;
        public static Coin BitcoinCash;
        public static Coin EthereumClassic;
        public static Coin Steem;
        public static Coin SteemDollar;
        public static Coin Stellar;
        public static Coin DigiByte;
        public static Coin District0x;
        public static Coin Ripple;
        public static Coin Monero;
        public static Coin Dash;
        public static Coin Einsteinium;
        public static Coin NEM;
        public static Coin Cardano;
        public static Coin PowerLedger;
        public static Coin Nexus;
        public static Coin TRON;
        public static Coin EOS;
        public static Coin DigixDAO;

        static CoinFactory() {
            Bitcoin = new Coin(1, "bitcoin", "Bitcoin", "BTC");
            Litecoin = new Coin(2, "litecoin", "Litecoin", "LTC");
            Ethereum = new Coin(3, "ethereum", "Ethereum", "ETH");
            Gridcoin = new Coin(4, "gridcoin", "Gridcoin", "GRC");
            Iota = new Coin(5, "iota", "IOTA", "IOTA");
            OmiseGo = new Coin(6, "omisego", "OmiseGo", "OMG");
            Bitshares = new Coin(7, "bitshares", "Bitshares", "BTS");
            Neo = new Coin(8, "neo", "NEO", "NEO");
            AdEx = new Coin(9, "adx-net", "AdEx", "ADX");
            BitcoinCash = new Coin(10, "bitcoin-cash", "BCash", "BCH");
            EthereumClassic = new Coin(11, "ethereum-classic", "Etherem-Cl.", "ETC");
            Steem = new Coin(12, "steem", "Steem", "STEEM");
            SteemDollar = new Coin(13, "steem-dollars", "Steem-Dollar", "SBD");
            Stellar = new Coin(14, "stellar", "Stellar Lum.", "XLM");
            DigiByte = new Coin(15, "digibyte", "DigiByte", "DGB");
            District0x = new Coin(16, "district0x", "District0x", "DNT");
            Ripple = new Coin(17, "ripple", "Ripple", "XRP");
            Monero = new Coin(18, "monero", "Monero", "XMR");
            Dash = new Coin(19, "dash", "Dash", "DASH");
            Einsteinium = new Coin(20, "einsteinium", "Einsteinium", "EMC2");
            NEM = new Coin(21, "nem", "NEM", "XEM");
            Cardano = new Coin(22, "cardano", "Cardano", "ADA");
            PowerLedger = new Coin(23, "power-ledger", "Power Ledger", "POWR");
            Nexus = new Coin(24, "nexus", "Nexus", "NXS");
            TRON = new Coin(25, "tron", "TRON", "TRX");
            EOS = new Coin(26, "eos", "EOS", "EOS");
            DigixDAO = new Coin(27, "digixdao", "DigixDAO", "DGD");
        }

        public static List<Coin> All() {
            return new List<Coin>() {
                Bitcoin, Litecoin, Ethereum, Gridcoin, Iota, OmiseGo, Bitshares, Neo, AdEx, BitcoinCash, EthereumClassic, Steem, SteemDollar, Stellar, DigiByte, District0x,
                Ripple, Monero, Dash, Einsteinium, NEM, Cardano, PowerLedger, Nexus, TRON, EOS, DigixDAO
            };
        }

    }
}
