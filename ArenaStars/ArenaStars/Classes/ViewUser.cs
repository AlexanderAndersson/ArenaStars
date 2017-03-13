using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArenaStars.Classes
{
    public class ViewUser
    {

        public long Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string SteamId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Country { get; set; }

        public DateTime? SignUpDate { get; set; }

        public DateTime? LastLoggedIn { get; set; }

        public bool IsAdmin { get; set; }

        public ArenaStars.Models.User.RankEnum Rank { get; set; }

        public int Level { get; set; }

        public int Elo { get; set; }

        public List<ViewTournament> Tournaments { get; set; }

        public List<ViewGame> Games { get; set; }

        public bool IsTerminated { get; set; }

        public string BanReason { get; set; }

        public DateTime? BanDuration { get; set; }

        public string ProfilePic { get; set; }

        public string BackgroundPic { get; set; }



        public int GamesCount { get; set; }

        public string LastFiveGamesScore { get; set; }

        public int placeInCountry { get; set; }

        public int placeInWorld { get; set; }

        public double winPercentage { get; set; }

    }
}