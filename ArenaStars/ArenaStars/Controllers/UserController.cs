using ArenaStars.Models;
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
                        ProfilePic = "~/Images/Profile/ProfilePicture_Default.jpg",
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

            return RedirectToAction("/Index", "Home");
        }

        public ActionResult Profile(string username)
        {
            object displayUser;

            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                var findUser = from u in context.Users
                               where username.ToLower() == u.Username
                               select u;

                if (findUser.Count() == 0)
                    return RedirectToAction("/UserNotFound", "User");

                User user = findUser.FirstOrDefault();

                displayUser = new
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    SteamId = user.SteamId,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Country = user.Country,
                    SignUpDate = user.SignUpDate,
                    LastLoggedIn = user.LastLoggedIn,
                    IsAdmin = user.IsAdmin,
                    Rank = user.Rank,
                    Level = user.Level,
                    Elo = user.Elo,
                    Tournaments = user.Tournaments,
                    Games = user.Games,
                    IsTerminated = user.IsTerminated,
                    BanReason = user.BanReason,
                    BanDuration = user.BanDuration,
                    ProfilePic = user.ProfilePic,
                    ReportList = user.ReportList
                };

            }
                
            return View(displayUser);
        }

        public ActionResult UserNotFound()
        {
            return View();
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
                    }
                }
            }


            return errorMsgList;
        }
    }
}