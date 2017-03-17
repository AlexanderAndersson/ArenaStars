using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public DateTime? SignUpDate { get; set; }

        public DateTime? LastLoggedIn { get; set; }

        public bool IsAdmin { get; set; }

        public RankEnum Rank { get; set; }

        public int Level { get; set; }

        public int Elo { get; set; }

        public virtual IList<Tournament> Tournaments { get; set; }

        public virtual IList<Game> Games { get; set; }

        public bool IsTerminated { get; set; }

        public string BanReason { get; set; }

        public DateTime? BanDuration { get; set; }

        public string ProfilePic { get; set; }

        public string BackgroundPic { get; set; }

        public virtual IList<Report> ReportList { get; set; }

        public enum RankEnum
        {
            Unranked = 1,
            Bronze = 2,
            Silver = 3,
            Gold = 4,
            Platinum = 5,
            Diamond = 6,
            Challenger = 7,
            Master = 8,
            Grandmaster = 9,
            Legend = 10
        }
    }
}