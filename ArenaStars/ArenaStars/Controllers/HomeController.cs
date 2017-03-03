using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArenaStars.Models;

namespace ArenaStars.Controllers
{
    public class HomeController : Controller
    {
        ArenaStarsContext context = new ArenaStarsContext();

        public ActionResult Index()
        {
            context.Database.Initialize(true);

            var rankedWinner = from g in context.Games
                                   //where g.Type == Game.GameTypeEnum.Ranked
                               select g.Winner;

            var tWinners = from t in context.Tournaments
                           select t.Winner;

            var nickesReports = from r in context.Reports
                                select r;

            ViewBag.Reports = nickesReports;
            ViewBag.TWinner = tWinners;
            ViewBag.Winner = rankedWinner;

            return View();
        }

        public ActionResult DummyData()
        {
            User u1 = new User()
            {
                #region info

                Username = "MuppetNicke",
                Firstname = "Niclas",
                Lastname = "Pettersson",
                Country = "Sweden",
                Email = "Nicke.Pettersson@gmail.com",
                Password = "hejsan",
                SignUpDate = DateTime.Now.AddDays(-20),
                LastLoggedIn = DateTime.Now,
                IsAdmin = false,
                Elo = 100,
                Rank = Models.User.RankEnum.Bronze,
                Level = 1,
                IsTerminated = false,
                SteamId = "123456789",
                ProfilePic = "https://pbs.twimg.com/profile_images/378800000330180958/13036c1c63bcdbdb64a2d773ff60c51a.jpeg"

                #endregion
            };

            User u2 = new User()
            {
                #region info

                Username = "AlleBalle",
                Firstname = "Alexander",
                Lastname = "Andersson",
                Country = "Sweden",
                Email = "Alexander.Andersson@gmail.com",
                Password = "hejsan",
                SignUpDate = DateTime.Now.AddDays(-10),
                LastLoggedIn = DateTime.Now,
                IsAdmin = true,
                Elo = 900,
                Rank = Models.User.RankEnum.Legend,
                Level = 9,
                IsTerminated = false,
                SteamId = "123456710",
                ProfilePic = "http://3.bp.blogspot.com/-CDMtRL7UoQM/U5oEt6uWX8I/AAAAAAAAJAM/Gl54j-00dNk/s1600/music-smiley.png"

                #endregion
            };

            List<User> TwoUsersList = new List<User>();
            List<User> UserList = new List<User>();
            List<Game> TournamentGameList = new List<Game>();
            List<GameStats> TwoGameStatsList = new List<GameStats>();
            List<GameStats> TournamentGameStatsList = new List<GameStats>();

            TwoUsersList.Add(u1);
            TwoUsersList.Add(u2);

            UserList.Add(u1);
            UserList.Add(u2);

            context.Users.Add(u1);
            context.Users.Add(u2);
            //context.SaveChanges();

            /****************GAMES*****************/

            Game RankedGame1 = new Game()
            {
                Participants = new List<User>()
                {
                    u1,u2
                },
                Winner = TwoUsersList.ElementAt(1),
                Map = "aim_map",
                Type = Game.GameTypeEnum.Ranked,
                GameStats = TwoGameStatsList
            };

            Game TournamentGame1 = new Game()
            {
                Participants = new List<User>()
                {
                    u1, u2
                },
                Winner = TwoUsersList.ElementAt(0),
                Map = "aim_map",
                Type = Game.GameTypeEnum.Tournament,
                GameStats = TournamentGameStatsList,
            };

            TournamentGameList.Add(TournamentGame1);

            context.Games.Add(TournamentGame1);
            context.Games.Add(RankedGame1);
            context.SaveChanges();

            /*************GAME STATS***************/

            GameStats GameStatsRankedPlayer1 = new GameStats()
            {
                Kills = 2,
                Deaths = 1,
                HsRatio = 0.75,
                SteamId = "123456789",
                Game = RankedGame1
            };

            GameStats GameStatsRankedPlayer2 = new GameStats()
            {
                Kills = 1,
                Deaths = 2,
                HsRatio = 0.45,
                SteamId = "123456710",
                Game = RankedGame1
            };

            GameStats GameStatsTournamentPlayer1 = new GameStats()
            {
                Kills = 3,
                Deaths = 0,
                HsRatio = 1,
                SteamId = "123456789",
                Game = TournamentGame1
            };

            GameStats GameStatsTournamentPlayer2 = new GameStats()
            {
                Kills = 0,
                Deaths = 3,
                HsRatio = 0,
                SteamId = "123456710",
                Game = TournamentGame1
            };

            TwoGameStatsList.Add(GameStatsRankedPlayer1);
            TwoGameStatsList.Add(GameStatsRankedPlayer2);

            TournamentGameStatsList.Add(GameStatsTournamentPlayer1);
            TournamentGameStatsList.Add(GameStatsTournamentPlayer2);

            RankedGame1.GameStats = TwoGameStatsList;
            TournamentGame1.GameStats = TournamentGameStatsList;

            context.GameStats.Add(GameStatsRankedPlayer1);
            context.GameStats.Add(GameStatsRankedPlayer2);
            context.GameStats.Add(GameStatsTournamentPlayer1);
            context.GameStats.Add(GameStatsTournamentPlayer2);
            context.SaveChanges();

            /***************TOURNAMENTS*****************/

            Tournament AllStarsTournament = new Tournament()
            {
                Name = "AllStars Only",
                Participants = UserList,
                Winner = UserList.ElementAt(1),
                Type = Tournament.TournamentTypeEnum.AllStars,
                CreatedDate = DateTime.Now,
                StartDate = DateTime.Now.AddDays(10),
                CheckInDate = DateTime.Now.AddDays(10).AddHours(10),
                HasEnded = false,
                IsLive = false,
                MinRank = Models.User.RankEnum.Legend,
                MaxRank = Models.User.RankEnum.Legend,
                PlayerLimit = 10,
                TrophyPic = "https://img.clipartfest.com/6bdf56bd13a3e1fe1a04b54876a1c4e9_trophy-clipart-trophy-clipart-images_1920-1920.jpeg",
                Games = TournamentGameList
            };

            context.Tournaments.Add(AllStarsTournament);
            context.SaveChanges();

            /********************REPORTS*********************/

            Report report1 = new Report()
            {
                ReportedUser = u1,
                Reportee = u2,
                Reason = Report.ReasonEnum.Cheating,
                Message = "This guy is cheatng for sure",
                SubmittedDate = DateTime.Now
            };

            Report report2 = new Report()
            {
                ReportedUser = u2,
                Reportee = u1,
                Reason = Report.ReasonEnum.Other,
                Message = "This guy reported me for nothing",
                SubmittedDate = DateTime.Now
            };

            u1.Tournaments.Add(AllStarsTournament);
            u2.Tournaments.Add(AllStarsTournament);

            context.Reports.Add(report1);
            context.Reports.Add(report2);
            context.SaveChanges();

            return RedirectToAction("/Index", "Home");
        }

    }
}