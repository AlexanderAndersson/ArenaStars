using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArenaStars.Models;
using ArenaStars.Classes;
using ArenaStars.Content;
using System.IO;
using QueryMaster;
using QueryMaster.GameServer;

namespace ArenaStars.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ArenaStarsContext context = new ArenaStarsContext();
            List<ViewTournament> tournaments = new List<ViewTournament>();
            List<ViewUser> users = new List<ViewUser>();

            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                context.Database.Initialize(true);

                var getTournaments = from t in context.Tournaments
                                  where t.HasEnded == false
                                  orderby t.IsLive descending
                                  select t;

                var playersWithHighestElo = from p in context.Users
                                            orderby p.Elo descending
                                            select p;

                var top10players = from p in context.Users
                                   orderby p.Elo descending
                                   select p;

                if ((bool)Session["isLoggedIn"] == true)
                {
                    string uname = Session["username"].ToString();
                    var getYou = from u in context.Users
                                 where u.Username.ToLower() == uname.ToLower()
                                 select u;
                    User you = getYou.FirstOrDefault();
                    var activeGame = from g in you.Games
                                     where g.HasEnded == false
                                     select g;

                    if (activeGame.Count() == 1)
                    {
                        Models.Game g = activeGame.FirstOrDefault();
                        ViewBag.activeGame = true;
                        ViewBag.activeGameId = g.Id;
                    }
                    
                }

                foreach (Tournament tournament in getTournaments.Take(5))
                {
                    tournaments.Add(
                        new ViewTournament()
                        {
                            Id = tournament.Id,
                            IsLive = tournament.IsLive,
                            StartDate = tournament.StartDate,
                            Name = tournament.Name,
                            Type = tournament.Type,
                            MinRank = tournament.MinRank,
                            MaxRank = tournament.MaxRank,
                            PlayersInTournament = tournament.Participants.Count,
                            PlayerLimit = tournament.PlayerLimit
                        });
                }

                foreach (User user in top10players.Take(7))
                {
                    users.Add(new ViewUser()
                    {
                        Username = user.Username,
                        Rank = user.Rank,
                        Elo = user.Elo
                    });
                }

                ViewBag.Top10Players = users;
                ViewBag.Top3HighestElo = playersWithHighestElo.Take(4).ToList();
                //ViewBag.Tournaments = tournaments.Take(5).ToList();
            }

            //Active state css ViewBag
            ViewBag.HomeSelected = "activeNav";

            return View(tournaments);
        }

        public ActionResult DummyData()
        {
            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                List<User> Tournament1UserList = new List<User>(); //Userlist for tournament 1
                List<Models.Game> Tournament1GameList = new List<Models.Game>(); //Gamelist for tournament 1

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
                    Elo = 1953,
                    Rank = Models.User.RankEnum.Master,
                    Level = 9,
                    IsTerminated = false,
                    SteamId = "1",
                    Bio = "Muppetnicke, the best player ever!",
                    ProfilePic = "/Images/Profile/nickebild.jpg",
                    BackgroundPic = "/Images/Profile/ProfileBackground_Default.jpg"

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
                    SteamId = "2",
                    Bio = "Certified noob",
                    ProfilePic = "/Images/Profile/rednaxBild.jpg",
                    BackgroundPic = "/Images/Profile/ProfileBackground_Default.jpg"

                    #endregion
                };

                User u3 = new User()
                {
                    #region info

                    Username = "LinusTechTips",
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
                    SteamId = "3",
                    ProfilePic = "https://scontent.xx.fbcdn.net/v/t31.0-8/901876_3921995267860_69458124_o.jpg?oh=2c7f7e218b0637a27e05885fed9f8157&oe=595B81FE",
                    BackgroundPic = "/Images/Profile/ProfileBackground_Default.jpg"

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
                    SteamId = "4",
                    ProfilePic = "https://upload.wikimedia.org/wikipedia/commons/e/e1/Stefan_L%C3%B6fven_efter_slutdebatten_i_SVT_2014_(cropped).jpg",
                    BackgroundPic = "/Images/Profile/ProfileBackground_Default.jpg"

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
                    SteamId = "5",
                    ProfilePic = "/Images/Profile/Olofmeister.jpg",
                    BackgroundPic = "/Images/Profile/ProfileBackground_Default.jpg"

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
                    Rank = Models.User.RankEnum.Challenger,
                    Level = 2,
                    IsTerminated = false,
                    SteamId = "6",
                    ProfilePic = "http://i48.tinypic.com/jkby0y.jpg",
                    BackgroundPic = "/Images/Profile/ProfileBackground_Default.jpg"

                    #endregion
                };

                User u7 = new User()
                {
                    #region info

                    Username = "KennyS",
                    Firstname = "Kenny",
                    Lastname = "Schrub",
                    Country = "France",
                    Email = "Kenny.Schrub@gmail.com",
                    Password = "hejsan",
                    SignUpDate = DateTime.Now.AddDays(-15),
                    LastLoggedIn = DateTime.Now,
                    IsAdmin = false,
                    Elo = 500,
                    Rank = Models.User.RankEnum.Grandmaster,
                    Level = 5,
                    IsTerminated = false,
                    SteamId = "7",
                    ProfilePic = "/Images/Profile/KennyS.jpeg",
                    BackgroundPic = "/Images/Profile/ProfileBackground_Default.jpg"

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
                    SteamId = "8",
                    ProfilePic = "https://scontent.xx.fbcdn.net/v/t1.0-9/17155817_1261089323927065_6180082253516590998_n.jpg?oh=9244719820e0cd51c53fc6b1f209ebf8&oe=5957A3F5",
                    BackgroundPic = "/Images/Profile/ProfileBackground_Default.jpg"

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
                    SteamId = "9",
                    ProfilePic = "/Images/Profile/GeT_RiGhT.jpg",
                    BackgroundPic = "/Images/Profile/ProfileBackground_Default.jpg"

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
                    SteamId = "10",
                    ProfilePic = "/Images/Profile/PashaBiceps.jpg",
                    BackgroundPic = "/Images/Profile/ProfileBackground_Default.jpg"

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
                    SteamId = "Admin",
                    ProfilePic = "/Images/Profile/ProfilePicture_Default.jpg",
                    BackgroundPic = "/Images/Profile/ProfileBackground_Default.jpg"

                    #endregion
                };

                User TerminatedUser1 = new Models.User()
                {
                    #region info

                    Username = "Jared",
                    Firstname = "Jared",
                    Lastname = "Fogle",
                    Country = "Sweden",
                    Email = "Jared.Fogle@prison.com",
                    Password = "hejsan",
                    SignUpDate = DateTime.Now.AddDays(-10),
                    LastLoggedIn = DateTime.Now,
                    IsAdmin = false,
                    Elo = 300,
                    Rank = Models.User.RankEnum.Bronze,
                    Level = 0,
                    IsTerminated = true,
                    BanFrom = DateTime.Now,
                    BanTo = DateTime.Now.AddYears(2),
                    BanReason = "Cheating",
                    SteamId = "terminated",
                    ProfilePic = "/Images/Profile/ProfilePicture_Default.jpg",
                    BackgroundPic = "/Images/Profile/ProfileBackground_Default.jpg",
                    Bio = "The ban is because my dad was using my account! This is his dad, yes I was cheating on his account please unban him you idiots!!!!"

                    #endregion
                };

                User TerminatedUser2 = new Models.User()
                {
                    #region info

                    Username = "ImBanned2",
                    Firstname = "Jared",
                    Lastname = "Fogle",
                    Country = "Sweden",
                    Email = "Jared.Fogle@prison.com",
                    Password = "hejsan",
                    SignUpDate = DateTime.Now.AddDays(-10),
                    LastLoggedIn = DateTime.Now,
                    IsAdmin = false,
                    Elo = 300,
                    Rank = Models.User.RankEnum.Bronze,
                    Level = 0,
                    IsTerminated = true,
                    BanFrom = DateTime.Now,
                    BanTo = DateTime.Now.AddMonths(2),
                    BanReason = "Cheating",
                    SteamId = "terminated",
                    ProfilePic = "/Images/Profile/ProfilePicture_Default.jpg",
                    BackgroundPic = "/Images/Profile/ProfileBackground_Default.jpg"

                    #endregion
                };

                User TerminatedUser3 = new Models.User()
                {
                    #region info

                    Username = "ImBanned3",
                    Firstname = "Jared",
                    Lastname = "Fogle",
                    Country = "Sweden",
                    Email = "Jared.Fogle@prison.com",
                    Password = "hejsan",
                    SignUpDate = DateTime.Now.AddDays(-10),
                    LastLoggedIn = DateTime.Now,
                    IsAdmin = false,
                    Elo = 300,
                    Rank = Models.User.RankEnum.Bronze,
                    Level = 0,
                    IsTerminated = true,
                    BanFrom = DateTime.Now,
                    BanTo = DateTime.Now.AddYears(2),
                    BanReason = "Cheating",
                    SteamId = "terminated",
                    ProfilePic = "/Images/Profile/ProfilePicture_Default.jpg",
                    BackgroundPic = "/Images/Profile/ProfileBackground_Default.jpg"

                    #endregion
                };

                User TerminatedUser4 = new Models.User()
                {
                    #region info

                    Username = "ImBanned4",
                    Firstname = "Jared",
                    Lastname = "Fogle",
                    Country = "Sweden",
                    Email = "Jared.Fogle@prison.com",
                    Password = "hejsan",
                    SignUpDate = DateTime.Now.AddDays(-10),
                    LastLoggedIn = DateTime.Now,
                    IsAdmin = false,
                    Elo = 300,
                    Rank = Models.User.RankEnum.Bronze,
                    Level = 0,
                    IsTerminated = true,
                    BanFrom = DateTime.Now,
                    BanTo = DateTime.Now.AddDays(9),
                    BanReason = "Cheating",
                    SteamId = "terminated",
                    ProfilePic = "/Images/Profile/ProfilePicture_Default.jpg",
                    BackgroundPic = "/Images/Profile/ProfileBackground_Default.jpg"

                    #endregion
                };

                #endregion

                //Adding Users to database
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
                context.Users.Add(TerminatedUser1);
                context.Users.Add(TerminatedUser2);
                context.Users.Add(TerminatedUser3);
                context.Users.Add(TerminatedUser4);

                //Adding Users in Userlist for tournament 1
                Tournament1UserList.Add(u1);
                Tournament1UserList.Add(u2);
                Tournament1UserList.Add(u3);
                Tournament1UserList.Add(u4);
                Tournament1UserList.Add(u5);
                Tournament1UserList.Add(u6);
                Tournament1UserList.Add(u7);
                Tournament1UserList.Add(u8);


                /*******************SERVERS**********************/

                #region Servers

                Models.Server serverOne = new Models.Server()
                {
                    IPaddress = "217.78.24.8:28892",
                    Name = "ArenaStars Server #1",
                    isInUse = false
                };

                Models.Server serverTwo = new Models.Server()
                {
                    IPaddress = "217.78.24.8:28892",
                    Name = "ArenaStars Server #2",
                    isInUse = false
                };

                Models.Server serverThree = new Models.Server()
                {
                    IPaddress = "217.78.24.8:28892",
                    Name = "ArenaStars Server #3",
                    isInUse = false
                };

                #endregion

                //Adding servers to database
                context.Servers.Add(serverOne);
                context.Servers.Add(serverTwo);
                context.Servers.Add(serverThree);


                /****************GAMES*****************/

                #region Games

                Models.Game RankedGame1 = new Models.Game()
                {
                    Participants = new List<User>() { u1, u2 },
                    Winner = u1,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Ranked,
                    PlayedDate = DateTime.Now.AddHours(-5),
                    HasEnded = true
                };

                Models.Game RankedGame2 = new Models.Game()
                {
                    Participants = new List<User>() { u2, u3 },
                    Winner = u2,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Ranked,
                    PlayedDate = DateTime.Now.AddMinutes(-30),
                    HasEnded = true
                };

                Models.Game RankedGame3 = new Models.Game()
                {
                    Participants = new List<User>() { u3, u4 },
                    Winner = u4,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Ranked,
                    PlayedDate = DateTime.Now.AddHours(-2),
                    HasEnded = true
                };

                Models.Game RankedGame4 = new Models.Game()
                {
                    Participants = new List<User>() { u4, u5 },
                    Winner = u4,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Ranked,
                    PlayedDate = DateTime.Now.AddHours(-1),
                    HasEnded = true
                };

                Models.Game RankedGame5 = new Models.Game()
                {
                    Participants = new List<User>() { u5, u6 },
                    Winner = u6,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Ranked,
                    PlayedDate = DateTime.Now.AddHours(-2),
                    HasEnded = true
                };

                Models.Game RankedGame6 = new Models.Game()
                {
                    Participants = new List<User>() { u6, u7 },
                    Winner = u6,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Ranked,
                    PlayedDate = DateTime.Now.AddHours(-2),
                    HasEnded = true
                };

                Models.Game RankedGame7 = new Models.Game()
                {
                    Participants = new List<User>() { u7, u8 },
                    Winner = u7,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Ranked,
                    PlayedDate = DateTime.Now.AddHours(-2),
                    HasEnded = true
                };

                Models.Game RankedGame8 = new Models.Game()
                {
                    Participants = new List<User>() { u8, u9 },
                    Winner = u9,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Ranked,
                    PlayedDate = DateTime.Now.AddHours(-2),
                    HasEnded = true
                };

                Models.Game RankedGame9 = new Models.Game()
                {
                    Participants = new List<User>() { u9, u10 },
                    Winner = u10,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Ranked,
                    PlayedDate = DateTime.Now.AddHours(-2),
                    HasEnded = true
                };

                Models.Game Tournament1Game1 = new Models.Game()
                {
                    Participants = new List<User>() { u1, u2 },
                    Winner = u1,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Tournament,
                    PlayedDate = DateTime.Now.AddHours(-2),
                    HasEnded = true
                };

                Models.Game Tournament1Game2 = new Models.Game()
                {
                    Participants = new List<User>() { u3, u4 },
                    Winner = u3,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Tournament,
                    PlayedDate = DateTime.Now.AddHours(-2),
                    HasEnded = true
                };

                Models.Game Tournament1Game3 = new Models.Game()
                {
                    Participants = new List<User>() { u5, u6 },
                    Winner = u6,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Tournament,
                    PlayedDate = DateTime.Now.AddHours(-2),
                    HasEnded = true
                };

                Models.Game Tournament1Game4 = new Models.Game()
                {
                    Participants = new List<User>() { u7, u8 },
                    Winner = u7,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Tournament,
                    PlayedDate = DateTime.Now,
                    HasEnded = true
                };

                Models.Game Tournament1Game5 = new Models.Game()
                {
                    Participants = new List<User>() { u9, u10 },
                    Winner = u10,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Tournament,
                    PlayedDate = DateTime.Now.AddHours(-2),
                    HasEnded = true
                };

                Models.Game Tournament1Game6 = new Models.Game()
                {
                    Participants = new List<User>() { u1, u3 },
                    Winner = u1,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Tournament,
                    PlayedDate = DateTime.Now.AddHours(-2),
                    HasEnded = true
                };

                Models.Game Tournament1Game7 = new Models.Game()
                {
                    Participants = new List<User>() { u6, u7 },
                    Winner = u6,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Tournament,
                    PlayedDate = DateTime.Now.AddHours(-2),
                    HasEnded = true
                };

                Models.Game Tournament1Game8 = new Models.Game()
                {
                    Participants = new List<User>() { u1, u6 },
                    Winner = u1,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Tournament,
                    PlayedDate = DateTime.Now.AddHours(-2),
                    HasEnded = true
                };

                Models.Game NotFinishedRankedGame1 = new Models.Game()
                {
                    Participants = new List<User>() { u1, u2 },
                    Winner = u1,
                    Map = "aim_map",
                    Type = Models.Game.GameTypeEnum.Ranked,
                    PlayedDate = DateTime.Now.AddHours(2),
                    HasEnded = false,
                    TournamentGameType = Models.Game.TournamentGameTypeEnum.Not_In_Tournament,
                    Server = serverOne
                };
                serverOne.isInUse = true;

                #endregion

                //Adding Ranked Games to database
                context.Games.Add(RankedGame1);
                context.Games.Add(RankedGame2);
                context.Games.Add(RankedGame3);
                context.Games.Add(RankedGame4);
                context.Games.Add(RankedGame5);
                context.Games.Add(RankedGame6);
                context.Games.Add(RankedGame7);
                context.Games.Add(RankedGame8);
                context.Games.Add(RankedGame9);
                context.Games.Add(NotFinishedRankedGame1);

                //Adding Tournament 1 Games to database
                context.Games.Add(Tournament1Game1);
                context.Games.Add(Tournament1Game2);
                context.Games.Add(Tournament1Game3);
                context.Games.Add(Tournament1Game4);
                context.Games.Add(Tournament1Game5);
                context.Games.Add(Tournament1Game6);
                context.Games.Add(Tournament1Game7);
                context.Games.Add(Tournament1Game8);

                //Adding Tournament 1 Games to list
                Tournament1GameList.Add(Tournament1Game1);
                Tournament1GameList.Add(Tournament1Game2);
                Tournament1GameList.Add(Tournament1Game3);
                Tournament1GameList.Add(Tournament1Game4);
                Tournament1GameList.Add(Tournament1Game5);
                Tournament1GameList.Add(Tournament1Game6);
                Tournament1GameList.Add(Tournament1Game7);
                Tournament1GameList.Add(Tournament1Game8);

                /*************GAME STATS***************/

                #region GameStats

                GameStats GameStatsRanked1Player1 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 0.75,
                    SteamId = "1",
                    Game = RankedGame1

                    #endregion
                };

                GameStats GameStatsRanked1Player2 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.45,
                    SteamId = "2",
                    Game = RankedGame1

                    #endregion
                };

                GameStats GameStatsRanked2Player1 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 0.5,
                    SteamId = "2",
                    Game = RankedGame2

                    #endregion
                };

                GameStats GameStatsRanked2Player2 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.45,
                    SteamId = "3",
                    Game = RankedGame2

                    #endregion
                };

                GameStats GameStatsRanked3Player1 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.50,
                    SteamId = "3",
                    Game = RankedGame3

                    #endregion
                };

                GameStats GameStatsRanked3Player2 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 1.00,
                    SteamId = "4",
                    Game = RankedGame3

                    #endregion
                };

                GameStats GameStatsRanked4Player1 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.45,
                    SteamId = "4",
                    Game = RankedGame4

                    #endregion
                };

                GameStats GameStatsRanked4Player2 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.45,
                    SteamId = "5",
                    Game = RankedGame4

                    #endregion
                };

                GameStats GameStatsRanked5Player1 = new GameStats()
                {
                    #region Info

                    Kills = 0,
                    Deaths = 3,
                    HsRatio = 0.00,
                    SteamId = "5",
                    Game = RankedGame5

                    #endregion
                };

                GameStats GameStatsRanked5Player2 = new GameStats()
                {
                    #region Info

                    Kills = 3,
                    Deaths = 0,
                    HsRatio = 1.00,
                    SteamId = "6",
                    Game = RankedGame5

                    #endregion
                };

                GameStats GameStatsRanked6Player1 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 0.50,
                    SteamId = "6",
                    Game = RankedGame6

                    #endregion
                };

                GameStats GameStatsRanked6Player2 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.00,
                    SteamId = "7",
                    Game = RankedGame6

                    #endregion
                };

                GameStats GameStatsRanked7Player1 = new GameStats()
                {
                    #region Info

                    Kills = 3,
                    Deaths = 0,
                    HsRatio = 0.33,
                    SteamId = "7",
                    Game = RankedGame7

                    #endregion
                };

                GameStats GameStatsRanked7Player2 = new GameStats()
                {
                    #region Info

                    Kills = 0,
                    Deaths = 3,
                    HsRatio = 0.00,
                    SteamId = "8",
                    Game = RankedGame7

                    #endregion
                };

                GameStats GameStatsRanked8Player1 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 1.00,
                    SteamId = "8",
                    Game = RankedGame8

                    #endregion
                };

                GameStats GameStatsRanked8Player2 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 0.50,
                    SteamId = "9",
                    Game = RankedGame8

                    #endregion
                };

                GameStats GameStatsRanked9Player1 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 0.00,
                    SteamId = "9",
                    Game = RankedGame9

                    #endregion
                };

                GameStats GameStatsRanked9Player2 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 0.50,
                    SteamId = "10",
                    Game = RankedGame9

                    #endregion
                };

                GameStats GS_T1_G1_P1 = new GameStats()
                {
                    #region Info

                    Kills = 3,
                    Deaths = 0,
                    HsRatio = 1,
                    SteamId = "1",
                    Game = Tournament1Game1

                    #endregion
                };

                GameStats GS_T1_G1_P2 = new GameStats()
                {
                    #region Info

                    Kills = 0,
                    Deaths = 3,
                    HsRatio = 0,
                    SteamId = "2",
                    Game = Tournament1Game1

                    #endregion
                };

                GameStats GS_T1_G2_P1 = new GameStats()
                {
                    #region Info

                    Kills = 3,
                    Deaths = 0,
                    HsRatio = 1,
                    SteamId = "3",
                    Game = Tournament1Game2

                    #endregion
                };

                GameStats GS_T1_G2_P2 = new GameStats()
                {
                    #region Info

                    Kills = 0,
                    Deaths = 3,
                    HsRatio = 0,
                    SteamId = "4",
                    Game = Tournament1Game2

                    #endregion
                };

                GameStats GS_T1_G3_P1 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 1,
                    SteamId = "5",
                    Game = Tournament1Game3

                    #endregion
                };

                GameStats GS_T1_G3_P2 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 0,
                    SteamId = "6",
                    Game = Tournament1Game3

                    #endregion
                };

                GameStats GS_T1_G4_P1 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 1,
                    SteamId = "7",
                    Game = Tournament1Game4

                    #endregion
                };

                GameStats GS_T1_G4_P2 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 1,
                    SteamId = "8",
                    Game = Tournament1Game4

                    #endregion
                };

                GameStats GS_T1_G5_P1 = new GameStats()
                {
                    #region Info

                    Kills = 0,
                    Deaths = 3,
                    HsRatio = 0.00,
                    SteamId = "9",
                    Game = Tournament1Game5

                    #endregion
                };

                GameStats GS_T1_G5_P2 = new GameStats()
                {
                    #region Info

                    Kills = 3,
                    Deaths = 0,
                    HsRatio = 0.50,
                    SteamId = "10",
                    Game = Tournament1Game5

                    #endregion
                };

                GameStats GS_T1_G6_P1 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 1.00,
                    SteamId = "1",
                    Game = Tournament1Game6

                    #endregion
                };

                GameStats GS_T1_G6_P2 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 1.00,
                    SteamId = "3",
                    Game = Tournament1Game6

                    #endregion
                };

                GameStats GS_T1_G7_P1 = new GameStats()
                {
                    #region Info

                    Kills = 2,
                    Deaths = 1,
                    HsRatio = 1.00,
                    SteamId = "6",
                    Game = Tournament1Game7

                    #endregion
                };

                GameStats GS_T1_G7_P2 = new GameStats()
                {
                    #region Info

                    Kills = 1,
                    Deaths = 2,
                    HsRatio = 1.00,
                    SteamId = "7",
                    Game = Tournament1Game7

                    #endregion
                };

                GameStats GS_T1_G8_P1 = new GameStats()
                {
                    #region Info

                    Kills = 0,
                    Deaths = 3,
                    HsRatio = 0.00,
                    SteamId = "6",
                    Game = Tournament1Game8

                    #endregion
                };

                GameStats GS_T1_G8_P2 = new GameStats()
                {
                    #region Info

                    Kills = 3,
                    Deaths = 0,
                    HsRatio = 1.00,
                    SteamId = "1",
                    Game = Tournament1Game8

                    #endregion
                };

                #endregion

                //Adding Ranked GameStats to database
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

                //Adding Tournament 1 GameStats to database
                context.GameStats.Add(GS_T1_G1_P1);
                context.GameStats.Add(GS_T1_G1_P2);
                context.GameStats.Add(GS_T1_G2_P1);
                context.GameStats.Add(GS_T1_G2_P2);
                context.GameStats.Add(GS_T1_G3_P1);
                context.GameStats.Add(GS_T1_G3_P2);
                context.GameStats.Add(GS_T1_G4_P1);
                context.GameStats.Add(GS_T1_G4_P2);
                context.GameStats.Add(GS_T1_G5_P1);
                context.GameStats.Add(GS_T1_G5_P2);
                context.GameStats.Add(GS_T1_G6_P1);
                context.GameStats.Add(GS_T1_G6_P2);
                context.GameStats.Add(GS_T1_G7_P1);
                context.GameStats.Add(GS_T1_G7_P2);
                context.GameStats.Add(GS_T1_G8_P1);
                context.GameStats.Add(GS_T1_G8_P2);

                /***************TOURNAMENTS*****************/

                #region Tournament

                Tournament Tournament1 = new Tournament()
                {
                    Name = "AllStars Only",
                    Participants = Tournament1UserList,
                    Type = Tournament.TournamentTypeEnum.AllStars,
                    CreatedDate = DateTime.Today.AddDays(9).Add(new TimeSpan(17, 00, 00)),
                    StartDate = DateTime.Today.AddDays(10).Add(new TimeSpan(15, 00, 00)),
                    CheckInDate = DateTime.Today.AddDays(10).Add(new TimeSpan(14, 30, 00)),
                    HasEnded = true,
                    IsLive = false,
                    MinRank = Models.User.RankEnum.Bronze,
                    MaxRank = Models.User.RankEnum.Legend,
                    PlayerLimit = 8,
                    Winner = Tournament1GameList.LastOrDefault().Winner,
                    TrophyPic = "/Images/Trophy/Trophy1.png",
                    Games = Tournament1GameList
                };

                Tournament Tournament2 = new Tournament()
                {
                    Name = "Midranks Only",
                    Participants = Tournament1UserList,
                    Type = Tournament.TournamentTypeEnum.Open,
                    CreatedDate = DateTime.Today.AddDays(9).Add(new TimeSpan(17, 00, 00)),
                    StartDate = DateTime.Today.AddDays(10).Add(new TimeSpan(15, 00, 00)),
                    CheckInDate = DateTime.Today.AddDays(10).Add(new TimeSpan(14, 30, 00)),
                    HasEnded = false,
                    IsLive = true,
                    MinRank = Models.User.RankEnum.Gold,
                    MaxRank = Models.User.RankEnum.Challenger,
                    PlayerLimit = 8,
                    TrophyPic = "/Images/Trophy/Trophy1.png",
                };

                Tournament Tournament3 = new Tournament()
                {
                    Name = "Noob Leauge",
                    Type = Tournament.TournamentTypeEnum.Open,
                    CreatedDate = DateTime.Today.AddDays(9).Add(new TimeSpan(17, 00, 00)),
                    StartDate = DateTime.Today.AddDays(10).Add(new TimeSpan(15, 00, 00)),
                    CheckInDate = DateTime.Today.AddDays(10).Add(new TimeSpan(14, 30, 00)),
                    HasEnded = false,
                    IsLive = false,
                    MinRank = Models.User.RankEnum.Bronze,
                    MaxRank = Models.User.RankEnum.Gold,
                    PlayerLimit = 12,
                    TrophyPic = "/Images/Trophy/Trophy2.png",
                };

                Tournament Tournament4 = new Tournament()
                {
                    Name = "Prove urself",
                    Type = Tournament.TournamentTypeEnum.Unproven,
                    CreatedDate = DateTime.Today.AddDays(9).Add(new TimeSpan(17, 00, 00)),
                    StartDate = DateTime.Today.AddDays(10).Add(new TimeSpan(15, 00, 00)),
                    CheckInDate = DateTime.Today.AddDays(10).Add(new TimeSpan(14, 30, 00)),
                    HasEnded = false,
                    IsLive = false,
                    MinRank = Models.User.RankEnum.Unranked,
                    MaxRank = Models.User.RankEnum.Legend,
                    PlayerLimit = 12,
                    TrophyPic = "/Images/Trophy/Trophy3.png",
                };

                Tournament Tournament5 = new Tournament()
                {
                    Name = "Come and play",
                    Participants = Tournament1UserList,
                    Type = Tournament.TournamentTypeEnum.Open,
                    CreatedDate = DateTime.Today.AddDays(9).Add(new TimeSpan(17, 00, 00)),
                    StartDate = DateTime.Today.AddDays(10).Add(new TimeSpan(15, 00, 00)),
                    CheckInDate = DateTime.Today.AddDays(10).Add(new TimeSpan(14, 30, 00)),
                    HasEnded = false,
                    IsLive = true,
                    MinRank = Models.User.RankEnum.Unranked,
                    MaxRank = Models.User.RankEnum.Legend,
                    PlayerLimit = 12,
                    TrophyPic = "/Images/Trophy/Trophy4.png",
                };

                Tournament Tournament6 = new Tournament()
                {
                    Name = "Legends Only",
                    Participants = Tournament1UserList,
                    Type = Tournament.TournamentTypeEnum.Open,
                    CreatedDate = DateTime.Today.AddDays(9).Add(new TimeSpan(17, 00, 00)),
                    StartDate = DateTime.Today.AddDays(10).Add(new TimeSpan(15, 00, 00)),
                    CheckInDate = DateTime.Today.AddDays(10).Add(new TimeSpan(14, 30, 00)),
                    HasEnded = false,
                    IsLive = true,
                    MinRank = Models.User.RankEnum.Legend,
                    MaxRank = Models.User.RankEnum.Legend,
                    PlayerLimit = 8,
                    TrophyPic = "/Images/Trophy/Trophy2.png",
                };

                #endregion

                //Adding tournaments to database
                context.Tournaments.Add(Tournament1);
                context.Tournaments.Add(Tournament2);
                context.Tournaments.Add(Tournament3);
                context.Tournaments.Add(Tournament4);
                context.Tournaments.Add(Tournament5);
                context.Tournaments.Add(Tournament6);

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

                //Adding reports to database
                context.Reports.Add(report1);
                context.Reports.Add(report2);
                context.Reports.Add(report3);


                //Saving changes to database
                context.SaveChanges();
            }
            return RedirectToAction("/Index", "Home");
        }

        public ActionResult UserSearch(string searchString)
        {
            List<ViewUser> userList = new List<ViewUser>();
            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                var getUserByUsername = (from u in context.Users
                                         where u.Username.Contains(searchString)
                                         orderby u.Username, u.Elo descending
                                         select u).Take(5);

                foreach (var user in getUserByUsername)
                {
                    ViewUser vUser = new ViewUser()
                    {
                        Username = user.Username,
                        ProfilePic = user.ProfilePic,
                        Country = user.Country,
                        RankString = user.Rank.ToString()
                    };
                    userList.Add(vUser);
                }
            }

            return Json(new { userList = userList }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult UserShowAll(string searchString)
        {
            List<ViewUser> userList = new List<ViewUser>();
            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                var getUserByUsername = (from u in context.Users
                                         where u.Username.Contains(searchString)
                                         orderby u.Username, u.Elo descending
                                         select u).Take(5);

                foreach (var user in getUserByUsername)
                {
                    ViewUser vUser = new ViewUser()
                    {
                        Username = user.Username,
                        ProfilePic = user.ProfilePic,
                        Country = user.Country,
                        RankString = user.Rank.ToString()
                    };
                    userList.Add(vUser);
                }
            }

            return View(userList);
        }

        public ActionResult StopMatchMakeSearch()
        {
            List<string> errorMessages = new List<string>();
            if ((bool)Session["isLoggedIn"] == true)
            {
                using (ArenaStarsContext context = new ArenaStarsContext())
                {
                    string uname = Session["username"].ToString();
                    var getQueueItem = from mm in context.MatchmakingSearches
                                       where mm.Username.ToLower() == uname.ToLower()
                                       select mm;

                    if (getQueueItem.Count() > 0)
                    {
                        context.MatchmakingSearches.Remove(getQueueItem.FirstOrDefault());
                        context.SaveChanges();
                    }
                    else
                    {
                        errorMessages.Add("You are not currently searching for a match!");
                    }
                }
            }
            else
            {
                errorMessages.Add("You must be logged in to exit queue!");
            }

            return Json(new { errors = errorMessages }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StartMatchMakeSearch()
        {
            List<string> errorMessages = new List<string>();
            User you;

            if ((bool)Session["isLoggedIn"] == true)
            {
                using (ArenaStarsContext context = new ArenaStarsContext())
                {
                    string uname = Session["username"].ToString();
                    //Gets your User.
                    var getYou = from u in context.Users
                                 where u.Username.ToLower() == uname.ToLower()
                                 select u;

                    you = getYou.FirstOrDefault();

                    //Checks if user is already in queue.
                    var checkIfAlreadyInQueue = from u in context.MatchmakingSearches
                                                where u.Username.ToLower() == you.Username.ToLower()
                                                select u;

                    if (you.IsTerminated)
                    {
                        errorMessages.Add("You are currently suspended and cannot queue for matchmaking at this time.");
                    }

                    if (checkIfAlreadyInQueue.Count() > 0)
                    {
                        errorMessages.Add("You are already in queue!");
                    }

                    //Check if user is already in a game.
                    foreach (var game in you.Games)
                    {
                        foreach (var participant in game.Participants)
                        {
                            if (participant.Username.ToLower() == you.Username.ToLower() && game.HasEnded == false)
                            {
                                errorMessages.Add("You cannot queue if you are already in a game!");
                                break;
                            }
                        }
                    }

                    if (errorMessages.Count == 0)
                    {
                        MatchmakingSearch searchReq = new MatchmakingSearch()
                        {
                            Username = you.Username,
                            Elo = you.Elo,
                            foundGame = false
                        };

                        context.MatchmakingSearches.Add(searchReq);
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                errorMessages.Add("You must be logged in to queue for a match!");
            }

            

            return Json(new { errors = errorMessages }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MatchMakeSearch(int timeSearched)
        {
            //Checks för att kunna söka.
            /*
             -Vara inloggad.
             -Inte vara bannad.
             -Kan inte redan söka.
             -Finnas server som inte är upptagen.
             */

            object myObject;
            List<string> errorMessages = new List<string>();
            User you = new Models.User();
            User opponent = new Models.User();
            MatchmakingSearch fakeYou;
            MatchmakingSearch fakeOpp;
            Models.Server chosenServer = new Models.Server()
            {
                Name = "NoServerFound",
                IPaddress = "0",
            };
            string uname = "";
            long gameId = 0;

            if ((bool)Session["isLoggedIn"] == true)
            {
                using (ArenaStarsContext context = new ArenaStarsContext())
                {
                    uname = Session["username"].ToString();
                    //Gets your User.
                    var getYouUser = from u in context.Users
                                where u.Username.ToLower() == uname.ToLower()
                                select u;

                    you = getYouUser.FirstOrDefault();

                    var checkIfQueueStarted = from mm in context.MatchmakingSearches
                                              where mm.Username.ToLower() == you.Username.ToLower()
                                              select mm;

                    if (checkIfQueueStarted.Count() != 1)
                    {
                        errorMessages.Add("You did not pass requisite checks!");
                    }
                }
            }
            else
            {
                errorMessages.Add("Not logged in!");
            }


            if (errorMessages.Count == 0)
            {
                using (ArenaStarsContext context = new ArenaStarsContext())
                {
                    uname = Session["username"].ToString();
                    //Gets your User.
                    var getYouUser = from u in context.Users
                                where u.Username.ToLower() == uname.ToLower()
                                select u;

                    you = getYouUser.FirstOrDefault();

                    int eloMinCap = you.Elo - timeSearched - 70;
                    int eloMaxCap = you.Elo + timeSearched + 70;

                    //Gets opponent based off of elo min & max cap, not same username (you) and not found game.
                    var possibleOpponents = from u in context.MatchmakingSearches
                                            where u.Elo > eloMinCap && u.Elo < eloMaxCap && u.Username.ToLower() != you.Username.ToLower() && u.foundGame == false
                                            orderby u.Elo descending
                                            select u;

                    if (possibleOpponents.Count() > 0)
                    {
                        //Gets available servers.
                        var getAvailableServers = from s in context.Servers
                                                  where s.isInUse == false
                                                  select s;


                        if (getAvailableServers.Count() < 1)
                        {
                            errorMessages.Add("No servers available at the moment. Please try again later.");
                        }
                        else
                        {
                            chosenServer = getAvailableServers.FirstOrDefault();
                        }
                        
                        if (errorMessages.Count == 0)
                        {
                            fakeOpp = possibleOpponents.FirstOrDefault();
                            fakeOpp.foundGame = true;

                            var getOpponentUser = from u in context.Users
                                             where u.Username.ToLower() == fakeOpp.Username.ToLower()
                                             select u;

                            opponent = getOpponentUser.FirstOrDefault();

                            var getFakeYou = from u in context.MatchmakingSearches
                                      where u.Username.ToLower() == you.Username.ToLower()
                                      select u;

                            fakeYou = getFakeYou.FirstOrDefault();

                            fakeYou.foundGame = true;

                            chosenServer.isInUse = true;

                            Models.Game newGame = new Models.Game()
                            {
                                Participants = new List<User>()
                                {
                                    you,
                                    opponent
                                },
                                PlayedDate = DateTime.Now,
                                Type = Models.Game.GameTypeEnum.Ranked,
                                Map = "aim_map",
                                TournamentGameType = Models.Game.TournamentGameTypeEnum.Not_In_Tournament,
                                HasEnded = false,
                                Server = chosenServer
                            };
                            you.Games.Add(newGame);
                            opponent.Games.Add(newGame);
                            context.MatchmakingSearches.Remove(fakeOpp);
                            context.MatchmakingSearches.Remove(fakeYou);
                        }
                        
                    }
                    else
                    {
                        errorMessages.Add("continue search");
                    }

                    context.SaveChanges();
                }
            }

            if (errorMessages.Count == 0)
            {
                var getGameId = from g in you.Games
                                where g.HasEnded == false
                                select g.Id;

                gameId = getGameId.FirstOrDefault();

                #region starServerShit


                Models.Game ggg = new Models.Game();
                ggg.Id = gameId;
                //GameLogsServiceReference1.Game logGame = new GameLogsServiceReference1.Game();
                //logGame.Id = gameId;
                string errorsPath = @"~/arenastars.net/Errors.txt";
                try
                {
                    Tools tool = new Tools();

                    tool.DoStuff(ggg);

                }
                catch (Exception ex)
                {
                    using (StreamWriter writer = new StreamWriter(errorsPath, true))
                    {
                        writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace + Environment.NewLine + "Innerexception :" + ex.InnerException +
                           "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                        writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    }
                    using (ArenaStarsContext context = new ArenaStarsContext())
                    {
                        var getGame = from g in context.Games
                                      where g.Id == gameId
                                      select g;
                        Models.Game ga = getGame.FirstOrDefault();
                        Models.User playerA = ga.Participants.FirstOrDefault();
                        Models.User playerB = ga.Participants.LastOrDefault();
                        string playerAID = "\"" + playerA.SteamId + "\"";
                        string playerBID = "\"" + playerB.SteamId + "\"";
                        QueryMaster.GameServer.Server server = ServerQuery.GetServerInstance(EngineType.Source, "217.78.24.8", 28892);

                        if (server.GetControl("lol"))
                        {
                            server.Rcon.SendCommand("sm_whitelist_remove " + playerAID);
                            server.Rcon.SendCommand("sm_whitelist_remove " + playerBID);
                            server.Rcon.SendCommand("sm_kick @all");
                            server.Rcon.SendCommand("changelevel aim_map");
                            server.Rcon.SendCommand("warmup");
                        }
                    }
                }


                #endregion

            }

            myObject = new
            {
                errors = errorMessages,
                gameId = gameId
            };

            return Json(new { searchData = myObject }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult CheckIfFoundGame()
        {
            bool foundGame = false;
            long gameId = 0;
            string uname = "";

            if ((bool)Session["isLoggedIn"])
            {
                uname = Session["username"].ToString();
                using (ArenaStarsContext context = new ArenaStarsContext())
                {
                    var getYouUser = from u in context.Users
                                     where u.Username.ToLower() == uname.ToLower()
                                     select u;

                    User you = getYouUser.FirstOrDefault();

                    var getActiveGames = from g in you.Games
                                         where g.HasEnded == false
                                         select g;

                    if (getActiveGames.Count() > 0)
                    {
                        Models.Game activeGame = getActiveGames.FirstOrDefault();

                        gameId = activeGame.Id;
                        foundGame = true;

                    }

                }
            }
            

            object response = new
            {
                gameId = gameId,
                foundGame = foundGame
            };
            return Json(new { response = response }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GameRoom(long gameId)
        {
            Models.Game game;
            ViewGame viewGame;
            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                var getGame = from g in context.Games
                              where g.Id == gameId
                              select g;

                game = getGame.FirstOrDefault();
                if (game.HasEnded == true)
                {
                    viewGame = new ViewGame()
                    {
                        Id = game.Id,
                        Server = game.Server,
                        Map = game.Map,
                        HasEnded = game.HasEnded,
                        Type = game.Type,
                        TournamentGameType = game.TournamentGameType,
                        PlayedDate = game.PlayedDate,
                        Participants = new List<ViewUser>()
                        {
                            new ViewUser()
                            {
                                Username = game.Participants.FirstOrDefault().Username,
                                ProfilePic = game.Participants.FirstOrDefault().ProfilePic,
                                Rank = game.Participants.FirstOrDefault().Rank,
                                Country = game.Participants.FirstOrDefault().Country,
                                Elo = game.Participants.FirstOrDefault().Elo,
                                SteamId = game.Participants.FirstOrDefault().SteamId
                            },
                            new ViewUser()
                            {
                                Username = game.Participants.LastOrDefault().Username,
                                ProfilePic = game.Participants.LastOrDefault().ProfilePic,
                                Rank = game.Participants.LastOrDefault().Rank,
                                Country = game.Participants.LastOrDefault().Country,
                                Elo = game.Participants.LastOrDefault().Elo,
                                SteamId = game.Participants.LastOrDefault().SteamId
                            }
                        },
                        Winner = new ViewUser
                        {
                            Username = game.Winner.Username,
                            ProfilePic = game.Winner.ProfilePic,
                            Rank = game.Winner.Rank,
                            Country = game.Winner.Country,
                            Elo = game.Winner.Elo,
                            SteamId = game.Winner.SteamId
                        },
                        GameStats = new List<ViewGamestat>()
                        {
                            new ViewGamestat()
                            {
                                SteamId = game.Participants.FirstOrDefault().SteamId,
                                Kills = game.GameStats.FirstOrDefault().Kills,
                                Deaths = game.GameStats.FirstOrDefault().Deaths,
                                HsRatio = game.GameStats.FirstOrDefault().HsRatio * 100
                            },
                            new ViewGamestat()
                            {
                                SteamId = game.Participants.LastOrDefault().SteamId,
                                Kills = game.GameStats.LastOrDefault().Kills,
                                Deaths = game.GameStats.LastOrDefault().Deaths,
                                HsRatio = game.GameStats.LastOrDefault().HsRatio * 100
                            }
                        }
                    };
                }
                else
                {
                    viewGame = new ViewGame()
                    {
                        Id = game.Id,
                        Server = game.Server,
                        Map = game.Map,
                        HasEnded = game.HasEnded,
                        Type = game.Type,
                        TournamentGameType = game.TournamentGameType,
                        Participants = new List<ViewUser>()
                        {
                            new ViewUser()
                            {
                                Username = game.Participants.FirstOrDefault().Username,
                                ProfilePic = game.Participants.FirstOrDefault().ProfilePic,
                                Rank = game.Participants.FirstOrDefault().Rank,
                                Country = game.Participants.FirstOrDefault().Country,
                                Elo = game.Participants.FirstOrDefault().Elo,
                                SteamId = game.Participants.FirstOrDefault().SteamId
                            },
                            new ViewUser()
                            {
                                Username = game.Participants.LastOrDefault().Username,
                                ProfilePic = game.Participants.LastOrDefault().ProfilePic,
                                Rank = game.Participants.LastOrDefault().Rank,
                                Country = game.Participants.LastOrDefault().Country,
                                Elo = game.Participants.LastOrDefault().Elo,
                                SteamId = game.Participants.LastOrDefault().SteamId
                            }
                        }
                    };
                    
                }
                
            }

                return View(viewGame);
        }
		
		
    }
}