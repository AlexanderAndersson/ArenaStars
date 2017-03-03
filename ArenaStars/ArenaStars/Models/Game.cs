using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArenaStars.Models
{
    public class Game
    {
        public long Id { get; set; }

        public GameTypeEnum Type { get; set; }

        public virtual IList<User> Participants { get; set; }

        public virtual User Winner { get; set; }

        public string Map { get; set; }

        public virtual IList<GameStats> GameStats { get; set; }

        public enum GameTypeEnum { Ranked, Unranked, Tournament, Challange }
    }
}