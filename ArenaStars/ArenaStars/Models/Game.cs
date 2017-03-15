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

        public DateTime? PlayedDate { get; set; }

        public bool HasEnded { get; set; }

        public string Map { get; set; }

        public virtual IList<GameStats> GameStats { get; set; }

        public TournamentGameTypeEnum TournamentGameType { get; set; }

        public enum GameTypeEnum { Ranked, Unranked, Tournament, Challange }

        public enum TournamentGameTypeEnum { Not_In_Tournament, Round_of_128, Round_of_64, Round_of_32, Round_of_16, Quarter_Final, Semi_Final, Final }
    }
}