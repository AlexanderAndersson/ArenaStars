using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArenaStars.Models
{
    public class Leaderboard
    {
        public long Id { get; set; }

        public virtual User User { get; set; }

        public int Elo { get; set; }

        public int Kills { get; set; }

        public int Deaths { get; set; }

        public double HsRatio { get; set; }

        public int Wins { get; set; }
    }
}