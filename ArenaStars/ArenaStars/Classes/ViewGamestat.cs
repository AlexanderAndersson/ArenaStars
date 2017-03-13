using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArenaStars.Classes
{
    public class ViewGamestat
    {
        public long Id { get; set; }

        public int Kills { get; set; }

        public int Deaths { get; set; }

        public double HsRatio { get; set; }

        public string SteamId { get; set; }

        public ViewGame Game { get; set; }
    }
}