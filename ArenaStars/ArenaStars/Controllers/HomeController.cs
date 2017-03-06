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

            var rankedGames = from g in context.Games
                              where g.Type == Game.GameTypeEnum.Ranked
                              select g;

            var tournamentGames = from t in context.Games
                                  where t.Type == Game.GameTypeEnum.Tournament
                                  select t;

            var tournamentList = from t in context.Tournaments
                                 select t;

            var reports = from r in context.Reports
                          select r;

            var userlist = from u in context.Users
                           select u;

            ViewBag.Tournaments = tournamentList;
            ViewBag.Users = userlist;
            ViewBag.Reports = reports;
            ViewBag.TGames = tournamentGames;
            ViewBag.RGames = rankedGames;

            //Active state css ViewBags
            ViewBag.HomeSelected = "activeNav";


            return View();
        }

        public ActionResult DummyData()
        {
            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                Random rnd = new Random();

                //List<User> GameOneUsersList = new List<User>();
                //List<User> GameTwoUsersList = new List<User>();
                List<User> UserList = new List<User>();
                List<Game> TournamentGameList = new List<Game>();

                /***************USER***************/

                #region Users

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
                    ProfilePic = "http://nairobiwire.com/wp-content/uploads/2016/11/linus.jpg"

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
                    ProfilePic = "http://www.southampton.ac.uk/assets/imported/transforms/site/staff-profile/Photo/C06EDCD541D84353B441425FD2B9EFE9/Stefan%20Bleeck.JPG_SIA_JPG_fit_to_width_INLINE.jpg"

                    #endregion
                };

                User u5 = new User()
                {
                    #region info

                    Username = "Olofmeister",
                    Firstname = "Olof",
                    Lastname = "Meister",
                    Country = "Sweden",
                    Email = "Olof.Meister@gmail.com",
                    Password = "hejsan",
                    SignUpDate = DateTime.Now.AddDays(-20),
                    LastLoggedIn = DateTime.Now,
                    IsAdmin = false,
                    Elo = 700,
                    Rank = Models.User.RankEnum.Master,
                    Level = 7,
                    IsTerminated = false,
                    SteamId = "123456410",
                    ProfilePic = "~/Images/Profile/ProfilePicture_Default.jpg"

                    #endregion
                };

                User u6 = new User()
                {
                    #region info

                    Username = "AW",
                    Firstname = "Arne",
                    Lastname = "Weise",
                    Country = "Sweden",
                    Email = "Arne.Weise@gmail.com",
                    Password = "hejsan",
                    SignUpDate = DateTime.Now.AddDays(-30),
                    LastLoggedIn = DateTime.Now,
                    IsAdmin = false,
                    Elo = 200,
                    Rank = Models.User.RankEnum.Challanger,
                    Level = 2,
                    IsTerminated = false,
                    SteamId = "123456310",
                    ProfilePic = "~/Images/Profile/ProfilePicture_Default.jpg"

                    #endregion
                };

                User u7 = new User()
                {
                    #region info

                    Username = "KennyS",
                    Firstname = "Kenny",
                    Lastname = "Schrub",
                    Country = "Sweden",
                    Email = "Kenny.Schrub@gmail.com",
                    Password = "hejsan",
                    SignUpDate = DateTime.Now.AddDays(-15),
                    LastLoggedIn = DateTime.Now,
                    IsAdmin = false,
                    Elo = 500,
                    Rank = Models.User.RankEnum.Grandmaster,
                    Level = 5,
                    IsTerminated = false,
                    SteamId = "123456210",
                    ProfilePic = "~/Images/Profile/ProfilePicture_Default.jpg"

                    #endregion
                };

                User u8 = new User()
                {
                    #region info

                    Username = "Khem",
                    Firstname = "Erik",
                    Lastname = "Westman",
                    Country = "Sweden",
                    Email = "Erik.Westman@gmail.com",
                    Password = "hejsan",
                    SignUpDate = DateTime.Now.AddDays(-50),
                    LastLoggedIn = DateTime.Now,
                    IsAdmin = false,
                    Elo = 100,
                    Rank = Models.User.RankEnum.Bronze,
                    Level = 1,
                    IsTerminated = false,
                    SteamId = "123456110",
                    ProfilePic = "~/Images/Profile/ProfilePicture_Default.jpg"

                    #endregion
                };

                User u9 = new User()
                {
                    #region info

                    Username = "Get_Right",
                    Firstname = "Christopher",
                    Lastname = "Alesund",
                    Country = "Sweden",
                    Email = "Christopher.Alesund@gmail.com",
                    Password = "hejsan",
                    SignUpDate = DateTime.Now.AddDays(-50),
                    LastLoggedIn = DateTime.Now,
                    IsAdmin = false,
                    Elo = 900,
                    Rank = Models.User.RankEnum.Legend,
                    Level = 9,
                    IsTerminated = false,
                    SteamId = "123456010",
                    ProfilePic = "~/Images/Profile/ProfilePicture_Default.jpg"

                    #endregion
                };

                User u10 = new User()
                {
                    #region info

                    Username = "PashaBiceps",
                    Firstname = "Jarosław",
                    Lastname = "Jarząbkowski",
                    Country = "Poland",
                    Email = "Jarosław.Jarząbkowski@gmail.com",
                    Password = "hejsan",
                    SignUpDate = DateTime.Now.AddDays(-60),
                    LastLoggedIn = DateTime.Now,
                    IsAdmin = false,
                    Elo = 900,
                    Rank = Models.User.RankEnum.Legend,
                    Level = 9,
                    IsTerminated = false,
                    SteamId = "123455910",
                    ProfilePic = "~/Images/Profile/ProfilePicture_Default.jpg"

                    #endregion
                };

                User admin = new User()
                {
                    #region info

                    Username = "Admin",
                    Firstname = "Admin",
                    Lastname = "Admin",
                    Country = "Sweden",
                    Email = "Admin.Admin@gmail.com",
                    Password = "hejsan",
                    SignUpDate = DateTime.Now.AddDays(-100),
                    LastLoggedIn = DateTime.Now,
                    IsAdmin = true,
                    Elo = 0,
                    Rank = Models.User.RankEnum.Unranked,
                    Level = 0,
                    IsTerminated = false,
                    SteamId = "",
                    ProfilePic = "~/Images/Profile/ProfilePicture_Default.jpg"

                    #endregion
                };

                #endregion

                context.Users.Add(u1);
                context.Users.Add(u2);
                context.Users.Add(u3);
                context.Users.Add(u4);
                context.Users.Add(u5);
                context.Users.Add(u6);
                context.Users.Add(u7);
                context.Users.Add(u8);
                context.Users.Add(u9);
                context.Users.Add(u10);
                context.Users.Add(admin);

                //GameOneUsersList.Add(u1);
                //GameOneUsersList.Add(u2);

                //GameTwoUsersList.Add(u3);
                //GameTwoUsersList.Add(u4);

                UserList.Add(u1);
                UserList.Add(u2);
                UserList.Add(u3);
                UserList.Add(u4);
                UserList.Add(u5);
                UserList.Add(u6);
                UserList.Add(u7);
                UserList.Add(u8);
                //UserList.Add(u9);
                //UserList.Add(u10);


                /****************GAMES*****************/

                #region Games

                Game RankedGame1 = new Game()
                {
                    Participants = new List<User>() { u1, u2 },
                    Winner = u1,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Ranked,
                };

                Game RankedGame2 = new Game()
                {
                    Participants = new List<User>() { u2, u3 },
                    Winner = u2,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Ranked,
                };

                Game RankedGame3 = new Game()
                {
                    Participants = new List<User>() { u3, u4 },
                    Winner = u4,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Ranked,
                };

                Game RankedGame4 = new Game()
                {
                    Participants = new List<User>() { u4, u5 },
                    Winner = u4,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Ranked,
                };

                Game RankedGame5 = new Game()
                {
                    Participants = new List<User>() { u5, u6 },
                    Winner = u6,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Ranked,
                };

                Game RankedGame6 = new Game()
                {
                    Participants = new List<User>() { u6, u7 },
                    Winner = u6,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Ranked,
                };

                Game RankedGame7 = new Game()
                {
                    Participants = new List<User>() { u7, u8 },
                    Winner = u7,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Ranked,
                };

                Game RankedGame8 = new Game()
                {
                    Participants = new List<User>() { u8, u9 },
                    Winner = u9,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Ranked,
                };

                Game RankedGame9 = new Game()
                {
                    Participants = new List<User>() { u9, u10 },
                    Winner = u10,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Ranked,
                };

                Game TournamentGame1 = new Game()
                {
                    Participants = new List<User>() { u1, u2 },
                    Winner = u1,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Tournament,
                };

                Game TournamentGame2 = new Game()
                {
                    Participants = new List<User>() { u3, u4 },
                    Winner = u3,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Tournament,
                };

                Game TournamentGame3 = new Game()
                {
                    Participants = new List<User>() { u5, u6 },
                    Winner = u6,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Tournament,
                };

                Game TournamentGame4 = new Game()
                {
                    Participants = new List<User>() { u7, u8 },
                    Winner = u7,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Tournament,
                };

                Game TournamentGame5 = new Game()
                {
                    Participants = new List<User>() { u9, u10 },
                    Winner = u10,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Tournament,
                };

                Game TournamentGame6 = new Game()
                {
                    Participants = new List<User>() { u1, u3 },
                    Winner = u1,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Tournament,
                };

                Game TournamentGame7 = new Game()
                {
                    Participants = new List<User>() { u6, u7 },
                    Winner = u6,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Tournament,
                };

                Game TournamentGame8 = new Game()
                {
                    Participants = new List<User>() { u1, u6 },
                    Winner = u1,
                    Map = "aim_map",
                    Type = Game.GameTypeEnum.Tournament,
                };

                #endregion

                context.Games.Add(RankedGame1);
                context.Games.Add(RankedGame2);
                context.Games.Add(RankedGame3);
                context.Games.Add(RankedGame4);
                context.Games.Add(RankedGame5);
                context.Games.Add(RankedGame6);
                context.Games.Add(RankedGame7);
                context.Games.Add(RankedGame8);
                context.Games.Add(RankedGame9);

                context.Games.Add(TournamentGame1);
                context.Games.Add(TournamentGame2);
                context.Games.Add(TournamentGame3);
                context.Games.Add(TournamentGame4);
                context.Games.Add(TournamentGame5);
                context.Games.Add(TournamentGame6);
                context.Games.Add(TournamentGame7);
                context.Games.Add(TournamentGame8);

                TournamentGameList.Add(TournamentGame1);
                TournamentGameList.Add(TournamentGame2);
                TournamentGameList.Add(TournamentGame3);
                TournamentGameList.Add(TournamentGame4);
                TournamentGameList.Add(TournamentGame5);
                TournamentGameList.Add(TournamentGame6);
                TournamentGameList.Add(TournamentGame7);
                TournamentGameList.Add(TournamentGame8);

                /*************GAME STATS***************/

                #region GameStats

                GameStats GameStatsRanked1Player1 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 0.75,
                    SteamId = "123456789",
                    Game = RankedGame1

                    #endregion
                };

                GameStats GameStatsRanked1Player2 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.45,
                    SteamId = "123456710",
                    Game = RankedGame1

                    #endregion
                };

                GameStats GameStatsRanked2Player1 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 0.5,
                    SteamId = "123456610",
                    Game = RankedGame2

                    #endregion
                };

                GameStats GameStatsRanked2Player2 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.45,
                    SteamId = "123456510",
                    Game = RankedGame2

                    #endregion
                };

                GameStats GameStatsRanked3Player1 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.50,
                    SteamId = "123456610",
                    Game = RankedGame3

                    #endregion
                };

                GameStats GameStatsRanked3Player2 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 1.00,
                    SteamId = "123456510",
                    Game = RankedGame3

                    #endregion
                };

                GameStats GameStatsRanked4Player1 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.45,
                    SteamId = "123456510",
                    Game = RankedGame4

                    #endregion
                };

                GameStats GameStatsRanked4Player2 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.45,
                    SteamId = "123456410",
                    Game = RankedGame4

                    #endregion
                };

                GameStats GameStatsRanked5Player1 = new GameStats()
                {
                    #region Info

                    Kills = 0,
                    Deaths = 3,
                    HsRatio = 0.00,
                    SteamId = "123456410",
                    Game = RankedGame5

                    #endregion
                };

                GameStats GameStatsRanked5Player2 = new GameStats()
                {
                    #region Info

                    Kills = 3,
                    Deaths = 0,
                    HsRatio = 1.00,
                    SteamId = "123456310",
                    Game = RankedGame5

                    #endregion
                };

                GameStats GameStatsRanked6Player1 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 0.50,
                    SteamId = "123456310",
                    Game = RankedGame6

                    #endregion
                };

                GameStats GameStatsRanked6Player2 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.00,
                    SteamId = "123456210",
                    Game = RankedGame6

                    #endregion
                };

                GameStats GameStatsRanked7Player1 = new GameStats()
                {
                    #region Info

                    Kills = 3,
                    Deaths = 0,
                    HsRatio = 0.33,
                    SteamId = "123456210",
                    Game = RankedGame7

                    #endregion
                };

                GameStats GameStatsRanked7Player2 = new GameStats()
                {
                    #region Info

                    Kills = 0,
                    Deaths = 3,
                    HsRatio = 0.00,
                    SteamId = "123456110",
                    Game = RankedGame7

                    #endregion
                };

                GameStats GameStatsRanked8Player1 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 1.00,
                    SteamId = "123456110",
                    Game = RankedGame8

                    #endregion
                };

                GameStats GameStatsRanked8Player2 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 0.50,
                    SteamId = "123456010",
                    Game = RankedGame8

                    #endregion
                };

                GameStats GameStatsRanked9Player1 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.00,
                    SteamId = "123456010",
                    Game = RankedGame9

                    #endregion
                };

                GameStats GameStatsRanked9Player2 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 0.50,
                    SteamId = "123455910",
                    Game = RankedGame9

                    #endregion
                };

                GameStats GameStatsTournament1Player1 = new GameStats()
                {
                    #region Info

                    Kills = 3,
                    Deaths = 0,
                    HsRatio = 1,
                    SteamId = "123456789",
                    Game = TournamentGame1

                    #endregion
                };

                GameStats GameStatsTournament1Player2 = new GameStats()
                {
                    #region Info

                    Kills = 0,
                    Deaths = 3,
                    HsRatio = 0,
                    SteamId = "123456710",
                    Game = TournamentGame1

                    #endregion
                };

                #endregion

                context.GameStats.Add(GameStatsRanked1Player1);
                context.GameStats.Add(GameStatsRanked1Player2);
                context.GameStats.Add(GameStatsRanked2Player1);
                context.GameStats.Add(GameStatsRanked2Player2);
                context.GameStats.Add(GameStatsRanked3Player1);
                context.GameStats.Add(GameStatsRanked3Player2);
                context.GameStats.Add(GameStatsRanked4Player1);
                context.GameStats.Add(GameStatsRanked4Player2);
                context.GameStats.Add(GameStatsRanked5Player1);
                context.GameStats.Add(GameStatsRanked5Player2);
                context.GameStats.Add(GameStatsRanked6Player1);
                context.GameStats.Add(GameStatsRanked6Player2);
                context.GameStats.Add(GameStatsRanked7Player1);
                context.GameStats.Add(GameStatsRanked7Player2);
                context.GameStats.Add(GameStatsRanked8Player1);
                context.GameStats.Add(GameStatsRanked8Player2);
                context.GameStats.Add(GameStatsRanked9Player1);
                context.GameStats.Add(GameStatsRanked9Player2);

                context.GameStats.Add(GameStatsTournament1Player1);
                context.GameStats.Add(GameStatsTournament1Player2);

                /***************TOURNAMENTS*****************/

                Tournament AllStarsTournament = new Tournament()
                {
                    Name = "AllStars Only",
                    Participants = UserList,
                    Type = Tournament.TournamentTypeEnum.AllStars,
                    CreatedDate = DateTime.Now,
                    StartDate = DateTime.Now.AddDays(10).Add(new TimeSpan(11, 00, 00)),
                    CheckInDate = DateTime.Now.AddDays(10).Add(new TimeSpan(10, 30, 00)),
                    HasEnded = false,
                    IsLive = false,
                    MinRank = Models.User.RankEnum.Bronze,
                    MaxRank = Models.User.RankEnum.Legend,
                    PlayerLimit = 8,
                    Winner = TournamentGameList.LastOrDefault().Winner,
                    TrophyPic = "https://img.clipartfest.com/6bdf56bd13a3e1fe1a04b54876a1c4e9_trophy-clipart-trophy-clipart-images_1920-1920.jpeg",
                    Games = TournamentGameList
                };

                context.Tournaments.Add(AllStarsTournament);

                /********************REPORTS*********************/

                #region Reports

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

                #endregion

                context.Reports.Add(report1);
                context.Reports.Add(report2);
                context.Reports.Add(report3);
                context.SaveChanges();
            }
            return RedirectToAction("/Index", "Home");
        }
    }
}