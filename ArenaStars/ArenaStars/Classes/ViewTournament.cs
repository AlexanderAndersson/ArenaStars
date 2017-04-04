using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArenaStars.Classes
{
    public class ViewTournament
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? CheckInDate { get; set; }

        public DateTime? StartDate { get; set; }

        public bool IsLive { get; set; }

        public bool HasEnded { get; set; }

        public int PlayerLimit { get; set; }

        public List<ViewUser> Participants { get; set; }

        public int PlayersInTournament { get; set; }

        public string TrophyPic { get; set; }

        public List<ViewGame> Games { get; set; }

        public ArenaStars.Models.User.RankEnum MaxRank { get; set; }

        public ArenaStars.Models.User.RankEnum MinRank { get; set; }

        public ArenaStars.Models.Tournament.TournamentTypeEnum Type { get; set; }

        public List<ViewUser> InvitedPlayers { get; set; }

        public virtual ViewUser Winner { get; set; }

    }
}