using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class CoinMarketCapResult {

        public string ID { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int Rank { get; set; }
        public double? Price_btc { get; set; }
        public double? Price_usd { get; set; }
        public double? Price_eur { get; set; }
        [JsonProperty("24h_volume_usd")]
        public double? Volume_usd_24h_internal;
        public double? Volume_usd_24h { get { return Volume_usd_24h_internal; } }
        public double? Market_cap_usd { get; set; }
        public double? Available_supply { get; set; }
        public double? Total_supply { get; set; }
        public double? Percent_change_1h { get; set; }
        public double? Percent_change_24h { get; set; }
        public double? Percent_change_7d { get; set; }
        public int? Last_updated { get; set; }
    }
}
