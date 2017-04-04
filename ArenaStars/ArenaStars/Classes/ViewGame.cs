using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArenaStars.Classes
{
    public class ViewGame
    {
        public long Id { get; set; }

        public ArenaStars.Models.Game.GameTypeEnum Type { get; set; }

        public ArenaStars.Models.Game.TournamentGameTypeEnum TournamentGameType { get; set; }

        public List<ViewUser> Participants { get; set; }

        public ViewUser Winner { get; set; }

        public DateTime? PlayedDate { get; set; }

        public string Map { get; set; }

        public bool HasEnded { get; set; }

        public ArenaStars.Models.Server Server { get; set; }

        public List<ViewGamestat> GameStats { get; set; }
    }
}