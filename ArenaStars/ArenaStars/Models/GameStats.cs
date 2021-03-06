﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArenaStars.Models
{
    public class GameStats
    {
        public long Id { get; set; }

        public int Kills { get; set; }

        public int Deaths { get; set; }

        public double HsRatio { get; set; }

        public string SteamId { get; set; }

        public int Score { get; set; }

        public virtual Game Game { get; set; }
    }
}