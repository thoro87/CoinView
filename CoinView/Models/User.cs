﻿using System;
using System.Collections.Generic;

namespace CoinView.Models.Database
{
    public partial class User
    {
        public User()
        {
            Buys = new HashSet<Buy>();
            Creations = new HashSet<Creation>();
            Snapshots = new HashSet<Snapshot>();
            Trades = new HashSet<Trade>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }

        public ICollection<Buy> Buys { get; set; }
        public ICollection<Creation> Creations { get; set; }
        public ICollection<Snapshot> Snapshots { get; set; }
        public ICollection<Trade> Trades { get; set; }
    }
}
