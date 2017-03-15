using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArenaStars.Models
{
    public class MatchmakingSearch
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public int Elo { get; set; }

        public bool foundGame { get; set; }
    }
}