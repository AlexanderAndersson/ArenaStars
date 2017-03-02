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

        [Required(ErrorMessage = "Username cannot be empty!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password cannot be empty!")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 30.")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(
               @"^(?("")("".+?(?<!\\)""@)|(([0-9A-Za-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9A-Za-z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9A-Za-z][-\w]*[0-9A-Za-z]*\.)+[A-Za-z0-9][\-a-z0-9]{0,22}[A-Za-z0-9]))$",
               ErrorMessage = "Given E-mail address is not valid!")]
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

        public IList<Report> ReportList { get; set; }

        public enum RankEnum { Unranked, Bronze, Silver, Gold, Platinum, Diamond, Challanger, Master, Grandmaster, Legend }
    }
}