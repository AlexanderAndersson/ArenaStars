using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArenaStars.Models
{
    public class User
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string SteamId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Country { get; set; }

        public DateTime SignUpDate { get; set; }

        public DateTime LastLoggedIn { get; set; }

        public bool IsAdmin { get; set; }

        public RankEnum Ranks { get; set; }

        public int Level { get; set; }

        public int Elo { get; set; }

        public virtual IList<Tournament> TournamentsWon { get; set; }

        public virtual IList<Game> Games { get; set; }

        public bool IsTerminated { get; set; }

        public string ProfilePic { get; set; }

        public enum RankEnum {Unranked, Bronze, Silver, Gold, Platinum, Diamond, Challanger, Master, Grandmaster, Legend }
    }
}