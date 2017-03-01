using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArenaStars.Models
{
    public class Tournament
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public DateTime? Created{ get; set; }

        public DateTime? CheckInDate { get; set; }

        public DateTime? StartDate { get; set; }

        public bool IsLive { get; set; }

        public bool HasEnded { get; set; }

        public int PlayerLimit { get; set; }

        public IList<User> Participants { get; set; }

        public string TrophyPic { get; set; }

        public IList<Game> Games { get; set; }

        public User.RankEnum MaxRank { get; set; }

        public User.RankEnum MinRank { get; set; }

        public TournamentTypeEnum Type { get; set; }

        public IList<User> InvitedPlayers { get; set; }

        public virtual User Winner { get; set; }

        public enum TournamentTypeEnum { Veteran, AllStars, Open, Invite, Unproven }
    }
}