using ArenaStars.Models;
using ArenaStars.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace ArenaStars.Controllers
{
    public class UserController : Controller
    {

        [HttpPost]
        public ActionResult Register(string username, string email, string password, string password2)
        {
            Regex.Replace(username, @"\s+", "");  //Removes all white spaces.
            List<string> ErrorMsgList = checkRegisterInputFaults(username, email, password, password2);

            if (ErrorMsgList.Count == 0)
            {
                using (ArenaStarsContext context = new ArenaStarsContext())
                {
                    User newUser = new User()
                    {
                        Username = username,
                        Password = password,
                        Email = email,
                        SignUpDate = DateTime.Now,
                        IsAdmin = false,
                        IsTerminated = false,
                        LastLoggedIn = DateTime.Now,
                        ProfilePic = "~ProfilePicture_Default.jpg",
                        BackgroundPic = "ProfileBackground_Default.jpg",
                        Rank = Models.User.RankEnum.Unranked
                    };


                    Session["isLoggedIn"] = true;
                    Session["username"] = username;

                    context.Users.Add(newUser);
                    context.SaveChanges();
                }
            }


            return Json(new { errorList = ErrorMsgList }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            List<string> ErrorMsgList = checkLoginInputFaults(username, password);

            if (ErrorMsgList.Count == 0)
            {
                Session["isLoggedIn"] = true;
                //Session["username"] = "";  //Gör tilldelningen i CheckLoginInputFaults, var tyvärr tvungen att göra det ifall username ska vara "statiskt"

            }

            return Json(new { errorList = ErrorMsgList }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Logout()
        {
            Session["isLoggedIn"] = false;
            Session["username"] = "";
            Session["isAdmin"] = false;

            return RedirectToAction("/Index", "Home");
        }

        public new ActionResult Profile(string username)
        {
            User user = new Models.User();

            int gamesCount = 0;
            string lastFiveGamesScore = "";
            int placeInCountry = 0;
            int placeInWorld = 0;
            double winPercentage = 0;

            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                var findUser = from u in context.Users
                               where username.ToLower() == u.Username.ToLower()
                               select u;

                if (findUser.Count() == 0)
                {
                    context.Dispose();
                    return RedirectToAction("/UserNotFound", "User");
                }

                user = findUser.FirstOrDefault();

                gamesCount = user.Games.Where(g => g.HasEnded == true).Count();
                List<Game> lastFiveGames = user.Games.Take(5).Where(g => g.HasEnded == true).ToList();

                foreach (Game game in lastFiveGames)
                {
                    if (game.Winner.Username == user.Username)
                        lastFiveGamesScore += "W";
                    else
                        lastFiveGamesScore += "L";
                }

                while (lastFiveGamesScore.Length < 5)
                {
                    lastFiveGamesScore += "-";
                }

                List<string> getAllUsersCountry = (from u in context.Users
                                  where u.Country == user.Country
                                  select u.Username).ToList();

                var getAllUsersWorld = from u in context.Users
                                       select u.Username;

                for (int i = 0; i < getAllUsersCountry.Count(); i++)
                {
                    if (getAllUsersCountry.ElementAt(i) == user.Username)
                    {
                        placeInCountry = i + 1;
                        break;
                    }
                }

                int f = 0;
                foreach (string u in getAllUsersWorld)
                {
                    if (u == user.Username)
                    {
                        placeInWorld = f + 1;
                        break;
                    }
                    f++;
                }

                double tempWins = 0.0;
                foreach (Game game in user.Games)
                {
                    if (game.Winner.Username == user.Username)
                        tempWins++;
                }

                winPercentage = (tempWins / user.Games.Count) * 100;

            }
			
            ViewBag.ProfileSelected = "activeNav";
            ViewBag.ProfileNavSelected = "activeNav";

            // TODO: Ge placeInCountry & placeInWorld riktiga värden.

            ViewUser viewUser = new ViewUser()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                SteamId = user.SteamId,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Country = user.Country,
                Bio = user.Bio,
                SignUpDate = user.SignUpDate,
                LastLoggedIn = user.LastLoggedIn,
                IsAdmin = user.IsAdmin,
                Rank = user.Rank,
                Level = user.Level,
                Elo = user.Elo,
                IsTerminated = user.IsTerminated,
                BanReason = user.BanReason,
                BanFrom = user.BanFrom,
                BanTo = user.BanTo,
                ProfilePic = user.ProfilePic,
                BackgroundPic = user.BackgroundPic,
                
                GamesCount = gamesCount,
                LastFiveGamesScore = lastFiveGamesScore,
                placeInCountry = placeInCountry,
                placeInWorld = placeInWorld,
                winPercentage = winPercentage
            };

            return View(viewUser);
        }

        public ActionResult UserNotFound()
        {
            return View();
        }

        public ActionResult Settings()
        {

            if ((bool)Session["isLoggedIn"] == true)
            {
                string uname = Session["username"].ToString();
                using (ArenaStarsContext context = new ArenaStarsContext())
                {
                    string inputFirstname = Request["inputFirstname"];
                    string inputLastname = Request["inputLastname"];
                    string inputCountry = Request["inputCountry"];
                    string inputSteamId = Request["inputSteamId"];
                    string inputBio = Request["inputBio"];
                    string inputProfilePic = Request["inputProfilePic"];
                    string inputBackgroundPic = Request["inputBackgroundPic"];

                    var getUser = from u in context.Users
                                  where u.Username.ToLower() == uname.ToLower()
                                  select u;

                    User you = getUser.FirstOrDefault();

                    #region Input checks

                    if (string.IsNullOrEmpty(inputFirstname) == false)
                    {
                        you.Firstname = inputFirstname;
                    }

                    if (string.IsNullOrEmpty(inputLastname) == false)
                    {
                        you.Lastname = inputLastname;
                    }

                    if (string.IsNullOrEmpty(inputCountry) == false)
                    {
                        you.Country = inputCountry;
                    }

                    if (string.IsNullOrEmpty(inputSteamId) == false)
                    {
                        you.SteamId = inputSteamId;
                    }

                    if (string.IsNullOrEmpty(inputBio) == false)
                    {
                        you.Bio = inputBio;
                    }

                    if (string.IsNullOrEmpty(inputProfilePic) == false)
                    {
                        you.ProfilePic = inputProfilePic;
                    }

                    if (string.IsNullOrEmpty(inputBackgroundPic) == false)
                    {
                        you.BackgroundPic = inputBackgroundPic;
                    }

                    #endregion


                    context.SaveChanges();
                }

                using (ArenaStarsContext context = new ArenaStarsContext())
                {
                    //Set viewbag values

                    var getUser = from u in context.Users
                                  where u.Username.ToLower() == uname.ToLower()
                                  select u;

                    User you = getUser.FirstOrDefault();

                    ViewBag.Firstname = you.Firstname;
                    ViewBag.Lastname = you.Lastname;
                    ViewBag.Country = you.Country;
                    ViewBag.SteamId = you.SteamId;
                    ViewBag.Bio = you.Bio;
                    ViewBag.ProfilePic = you.ProfilePic;
                    ViewBag.BackgroundPic = you.BackgroundPic;
                }
            }
            else
            {
                return RedirectToAction("/Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult GetTournaments(int shown, string username)
        {
            List<object> tournaments = new List<object>();
            int numberToDisplay = 5; //Maybe(probably) make it parameter

            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                var findUser = from u in context.Users
                               where username.ToLower() == u.Username.ToLower()
                               select u;

                User user = findUser.FirstOrDefault();

                for (int i = shown; i < shown + numberToDisplay && i < user.Tournaments.Count; i++)
                {
                    var newTournament = new
                    {
                        CheckInDate = user.Tournaments[i].CheckInDate.ToString(),
                        CreatedDate = user.Tournaments[i].CreatedDate.ToString(),
                        StartDate = user.Tournaments[i].StartDate.ToString(),
                        HasEnded = user.Tournaments[i].HasEnded,
                        Id = user.Tournaments[i].Id,
                        IsLive = user.Tournaments[i].IsLive,
                        MaxRank = user.Tournaments[i].MaxRank.ToString(),
                        MinRank = user.Tournaments[i].MinRank.ToString(),
                        Name = user.Tournaments[i].Name,
                        PlayerLimit = user.Tournaments[i].PlayerLimit,
                        TrophyPic = user.Tournaments[i].TrophyPic,
                        Type = user.Tournaments[i].Type.ToString(),
                        ParticipantsCount = user.Tournaments[i].Participants.Count
                    };
                    tournaments.Add(newTournament);
                }

            }

            return Json(new { tournamentList = tournaments }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult GetGames(int shown, string username)
        {
            List<object> games = new List<object>();
            int numberToDisplay = 10; //Maybe(probably) make it parameter

            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                var findUser = from u in context.Users
                               where username.ToLower() == u.Username.ToLower()
                               select u;

                User user = findUser.FirstOrDefault();

                var finishedGames = user.Games.Where(g => g.HasEnded == true).ToList();

                for (int i = shown; i < shown + numberToDisplay && i < finishedGames.Count(); i++)
                {
                    var newGame = new
                    {
                        Id = finishedGames[i].Id,
                        Map = finishedGames[i].Map,
                        ParticipantOne = finishedGames[i].Participants.FirstOrDefault().Username,
                        ParticipantTwo = finishedGames[i].Participants.LastOrDefault().Username,
                        Type = finishedGames[i].Type.ToString(),
                        Winner = finishedGames[i].Winner.Username,
                        PlayedDate = finishedGames[i].PlayedDate.ToString(),
                        Kills = finishedGames[i].GameStats.FirstOrDefault().Kills,
                        Deaths = finishedGames[i].GameStats.FirstOrDefault().Deaths,
                        hasEnded = finishedGames[i].HasEnded
                    };
                    games.Add(newGame);
                }

            }

            return Json(new { gameList = games }, JsonRequestBehavior.DenyGet);
        }

        private List<string> checkRegisterInputFaults(string username, string email, string password, string password2)
        {
            List<string> errorMsgList = new List<string>();

            //Checks empty or whitespace
            if (string.IsNullOrWhiteSpace(username))
            {
                errorMsgList.Add("Username cannot be empty!");
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                errorMsgList.Add("E-mail cannot be empty!");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                errorMsgList.Add("Password cannot be empty!");
            }

            //Checks lengths
            if (username.Length < 3 || username.Length > 30)
            {
                errorMsgList.Add("Username must be between 3 and 30 characters.");
            }
            if (username.Length < 6 || username.Length > 30)
            {
                errorMsgList.Add("Password must be between 6 and 30 characters.");
            }

            //Checks if passwords match
            if (password2 != password)
            {
                errorMsgList.Add("Given passwords do not match!");
            }

            //Regex check
            string pattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9A-Za-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9A-Za-z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9A-Za-z][-\w]*[0-9A-Za-z]*\.)+[A-Za-z0-9][\-a-z0-9]{0,22}[A-Za-z0-9]))$";
            if (!Regex.IsMatch(email, pattern))
            {
                errorMsgList.Add("Given email address is not valid!");
            }


            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                var checkUserExists = from u in context.Users
                                      where username.ToLower() == u.Username.ToLower()
                                      select u;

                var checkEmailExists = from u in context.Users
                                       where email.ToLower() == u.Email.ToLower()
                                       select u;

                //Checks if user with same username exists
                if (checkUserExists.Count() > 0)
                {
                    errorMsgList.Add("Given username is taken!");
                }

                //Checks if user with same email exists
                if (checkEmailExists.Count() > 0)
                {
                    errorMsgList.Add("Given email is taken!");
                }
            }

            return errorMsgList;
        }

        private List<string> checkLoginInputFaults(string username, string password)
        {
            List<string> errorMsgList = new List<string>();

            //Kollar ifall det finns en användare med angivna username och password.
            //Uppdatera last logged in date.

            //Checks if inputs are empty, null or whitespace
            if (string.IsNullOrWhiteSpace(username))
            {
                errorMsgList.Add("Username field is empty!");
            }
            //Checks if password is empty, null or whitespace
            if (string.IsNullOrWhiteSpace(password))
            {
                errorMsgList.Add("Password field is empty!");
            }

            if (errorMsgList.Count == 0)
            {
                using (ArenaStarsContext context = new ArenaStarsContext())
                {
                    var findUser = from u in context.Users
                                   where username.ToLower() == u.Username.ToLower() && password == u.Password
                                   select u;

                    if (findUser.Count() == 0)
                    {
                        errorMsgList.Add("There is no user with given username and password combination");
                    }
                    else
                    {
                        User u = findUser.FirstOrDefault();
                        u.LastLoggedIn = DateTime.Now;
                        Session["username"] = u.Username;
                        Session["isAdmin"] = u.IsAdmin;
                        Session["profilePictureUrl"] = u.ProfilePic;
                    }
                }
            }


            return errorMsgList;
        }
    }
}