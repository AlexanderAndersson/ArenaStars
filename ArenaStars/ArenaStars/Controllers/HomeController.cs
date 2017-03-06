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
        public ActionResult Index()
        {
            ArenaStarsContext context = new ArenaStarsContext();
                 
                context.Database.Initialize(true);

                var games = from g in context.Games
                                   select g;

                var tWinners = from t in context.Tournaments
                               select t.Winner;

                var reports = from r in context.Reports
                                    select r;

                ViewBag.Reports = reports;
                ViewBag.TWinner = tWinners;
                ViewBag.Games = games;     
                      
            return View();
        }

        public ActionResult DummyData()
        {
            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                List<User> GameOneUsersList = new List<User>();
                List<User> GameTwoUsersList = new List<User>();
                List<User> UserList = new List<User>();
                List<Game> TournamentGameList = new List<Game>();

                /***************USER***************/

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
                    ProfilePic = "https://pbs.twimg.com/profile_images/378800000330180958/13036c1c63bcdbdb64a2d773ff60c51a.jpeg",
                    BackgroundPic = "~/Images/Profile/ProfileBackground_Default.jpg"

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
                    ProfilePic = "http://3.bp.blogspot.com/-CDMtRL7UoQM/U5oEt6uWX8I/AAAAAAAAJAM/Gl54j-00dNk/s1600/music-smiley.png",
                    BackgroundPic = "~/Images/Profile/ProfileBackground_Default.jpg"

                    #endregion
                };

                User u3 = new User()
                {
                    #region info

                    Username = "linustechtips",
                    Firstname = "Linus",
                    Lastname = "Eriksson",
                    Country = "Sweden",
                    Email = "linus.eriksson@gmail.com",
                    Password = "hejsan",
                    SignUpDate = DateTime.Now.AddDays(-10),
                    LastLoggedIn = DateTime.Now,
                    IsAdmin = false,
                    Elo = 500,
                    Rank = Models.User.RankEnum.Gold,
                    Level = 5,
                    IsTerminated = false,
                    SteamId = "123456610",
                    ProfilePic = "http://nairobiwire.com/wp-content/uploads/2016/11/linus.jpg",
                    BackgroundPic = "~/Images/Profile/ProfileBackground_Default.jpg"

                    #endregion
                };

                User u4 = new User()
                {
                    #region info

                    Username = "Steffe",
                    Firstname = "Stefan",
                    Lastname = "Larsson",
                    Country = "Sweden",
                    Email = "stefan.larsson@gmail.com",
                    Password = "hejsan",
                    SignUpDate = DateTime.Now.AddDays(-10),
                    LastLoggedIn = DateTime.Now,
                    IsAdmin = false,
                    Elo = 400,
                    Rank = Models.User.RankEnum.Silver,
                    Level = 4,
                    IsTerminated = false,
                    SteamId = "123456510",
                    ProfilePic = "http://www.southampton.ac.uk/assets/imported/transforms/site/staff-profile/Photo/C06EDCD541D84353B441425FD2B9EFE9/Stefan%20Bleeck.JPG_SIA_JPG_fit_to_width_INLINE.jpg",
                    BackgroundPic = "~/Images/Profile/ProfileBackground_Default.jpg"

                    #endregion
                };

                context.Users.Add(u1);
                context.Users.Add(u2);
                context.Users.Add(u3);
                context.Users.Add(u4);

                GameOneUsersList.Add(u1);
                GameOneUsersList.Add(u2);

                GameTwoUsersList.Add(u3);
                GameTwoUsersList.Add(u4);

                UserList.Add(u1);
                UserList.Add(u2);

                /****************GAMES*****************/

                Game RankedGame1 = new Game()
                {
                    Participants = GameOneUsersList,
                    Winner = GameOneUsersList.ElementAt(1),
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Ranked,
                };

                Game RankedGame2 = new Game()
                {
                    Participants = GameTwoUsersList,
                    Winner = GameTwoUsersList.ElementAt(0),
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Ranked,
                };

                Game TournamentGame1 = new Game()
                {
                    Participants = GameOneUsersList,
                    Winner = GameOneUsersList.ElementAt(0),
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Tournament,
                };

                context.Games.Add(TournamentGame1);
                context.Games.Add(RankedGame1);
                context.Games.Add(RankedGame2);

                TournamentGameList.Add(TournamentGame1);

                /*************GAME STATS***************/

                GameStats GameStatsFirstRankedPlayer1 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 0.75,
                    SteamId = "123456789",
                    Game = RankedGame1

                    #endregion
                };

                GameStats GameStatsFirstRankedPlayer2 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.45,
                    SteamId = "123456710",
                    Game = RankedGame1

                    #endregion
                };

                GameStats GameStatsSecondRankedPlayer1 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 0.5,
                    SteamId = "123456610",
                    Game = RankedGame2

                    #endregion
                };

                GameStats GameStatsSecondRankedPlayer2 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.45,
                    SteamId = "123456510",
                    Game = RankedGame2

                    #endregion
                };

                GameStats GameStatsTournamentPlayer1 = new GameStats()
                {
                    #region Info

                    Kills = 3,
                    Deaths = 0,
                    HsRatio = 1,
                    SteamId = "123456789",
                    Game = TournamentGame1

                    #endregion
                };

                GameStats GameStatsTournamentPlayer2 = new GameStats()
                {
                    #region Info

                    Kills = 0,
                    Deaths = 3,
                    HsRatio = 0,
                    SteamId = "123456710",
                    Game = TournamentGame1

                    #endregion
                };

                context.GameStats.Add(GameStatsFirstRankedPlayer1);
                context.GameStats.Add(GameStatsFirstRankedPlayer2);
                context.GameStats.Add(GameStatsSecondRankedPlayer1);
                context.GameStats.Add(GameStatsSecondRankedPlayer2);
                context.GameStats.Add(GameStatsTournamentPlayer1);
                context.GameStats.Add(GameStatsTournamentPlayer2);

                /***************TOURNAMENTS*****************/

                Tournament AllStarsTournament = new Tournament()
                {
                    Name = "AllStars Only",
                    Participants = UserList,
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

                Report report3 = new Report()
                {
                    ReportedUser = u3,
                    Reportee = u1,
                    Reason = Report.ReasonEnum.Toxic,
                    Message = "To much tech tips",
                    SubmittedDate = DateTime.Now
                };

                context.Reports.Add(report1);
                context.Reports.Add(report2);
                context.Reports.Add(report3);
                context.SaveChanges();
            }
            return RedirectToAction("/Index", "Home");
        }
    }
}