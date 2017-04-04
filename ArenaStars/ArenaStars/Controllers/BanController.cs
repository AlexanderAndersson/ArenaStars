using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArenaStars.Models;
using ArenaStars.Classes;

namespace ArenaStars.Controllers
{
    public class BanController : Controller
    {
        public ActionResult Index()
        {
            //Active state css ViewBags
            ViewBag.BanlistSelected = "activeNav";
            //List<User> getAllBannedUsers = new List<Models.User>();
            List<ViewUser> users = new List<ViewUser>();

            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                var getAllBannedUsers = (from u in context.Users
                                        where u.IsTerminated == true
                                        orderby u.BanFrom descending
                                        select u).ToList();

                foreach (User user in getAllBannedUsers)
                {
                    DateTime banFrom = user.BanFrom.Value;
                    DateTime banTo = user.BanTo.Value;
                    DateTime startDate = user.SignUpDate.Value;
                    DateTime today = DateTime.Now;

                    TimeSpan difference = banTo - banFrom;
                    TimeSpan difference2 = today - startDate;

                    var banDays = difference.TotalDays;
                    var memberDays = difference2.TotalDays;

                    users.Add(new ViewUser()
                    {
                        Username = user.Username,
                        Rank = user.Rank,
                        ProfilePic = user.ProfilePic,
                        BanExpires = banDays,
                        DaysAsMember = memberDays,
                        BanFrom = user.BanFrom.Value,
                        BanTo = user.BanTo.Value,
                        BanReason = user.BanReason
                    });
                }

                ViewBag.BannedUsers = users;
            }
                
            return View();
        }
    }
}