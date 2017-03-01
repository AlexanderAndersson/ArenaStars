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
        // GET: User
        public ActionResult Register(string username, string email, string password, string password2)
        {
            List<string> ErrorMsgList = checkRegisterInputFaults(username, email, password, password2);
                

            //Kolla ifall man måste kolla efter white spaces endast.

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

            if (ErrorMsgList.Count == 0)
                return RedirectToAction("/Index", "Home");
            else
            {
                return Json(new { errorList = ErrorMsgList }, JsonRequestBehavior.DenyGet);
            }

        }

        public ActionResult Login()
        {
            bool foundFaults = false;
            List<string> ErrorMsgList = new List<string>();


            string username = Request["inputUsername"];
            string password = Request["inputPassword"];

            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                var findUser = from u in context.Users
                                where username.ToLower() == u.Username.ToLower()
                                select u; //Gets user with inputted username


                if (findUser.Count() <= 0) //Checks if user exists with inputted username
                {
                    foundFaults = true;
                    ErrorMsgList.Add("There is no user with that name");
                }
                else
                {
                    User user = findUser.FirstOrDefault();
                    if (password == user.Password)
                    {
                        //Login succeed
                        Session["isLoggedIn"] = true;
                        Session["username"] = username;

                        user.LastLoggedIn = DateTime.Now;
                    }
                    else
                    {
                        //Login failed
                        foundFaults = true;
                        ErrorMsgList.Add("Wring username or password!");
                    }
                }
            }


            if (!foundFaults)
                return RedirectToAction("/Index", "Home");
            else
                return Json(new { errorList = ErrorMsgList }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Logout()
        {
            Session["isLoggedIn"] = false;
            Session["username"] = "";

            return RedirectToAction("/Index", "Home");
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
            if (username.Length >= 3 && username.Length <= 30)
            {
                errorMsgList.Add("Username must be between 3 and 30 characters.");
            }
            if (username.Length >= 6 && username.Length <= 30)
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
            if (Regex.IsMatch(email, pattern))
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
    }
}