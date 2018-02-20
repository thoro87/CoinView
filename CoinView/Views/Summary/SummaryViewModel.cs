using CoinView.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public class SummaryViewModel {

        public InfoAreaDO InfoArea { get; set; }
        public List<SummaryDO> UserSummaries { get; set; }
        public SummaryDO CombinedSummary { get; set; }
    }
}
