using ArenaStars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArenaStars.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Register()
        {
            bool foundFaults = false;
            List<string> ErrorMsgList = new List<string>();

            if (ModelState.IsValid)
            {
                string username = Request["inputUsername"];
                string password = Request["inputPassword"];
                string password2 = Request["inputPassword2"];
                string email = Request["InputEmail"];

                if (password2 != password)
                {
                    ErrorMsgList.Add("Given passwords do not match!");
                    ViewBag.passwordNotMatch = "Given passwords do not match!";
                    foundFaults = true;
                }
                

                //Kolla ifall man måste kolla efter white spaces endast.

                if (!foundFaults)
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
            }

            if (!foundFaults)
                return RedirectToAction("/Index", "Home");
            else
            {
                //return Json(new { errorList = ErrorMsgList }, JsonRequestBehavior.DenyGet);
                return View();
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

        private List<string> checkRegisterInputFaults()
        {
            /*
             
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
             
             */


            return null;
        }
    }
}