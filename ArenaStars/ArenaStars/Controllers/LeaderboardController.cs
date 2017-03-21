using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArenaStars.Models;
using ArenaStars.Classes;

namespace ArenaStars.Controllers
{
    public class LeaderboardController : Controller
    {
        public ActionResult Index()
        {
            List<ViewUser> users = new List<ViewUser>();
            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                var topPlayers = from p in context.Users
                                 orderby p.Elo descending
                                 where p.IsTerminated != true
                                 select p;

                foreach (User user in topPlayers)
                {
                    DateTime startDate = user.SignUpDate.Value;
                    DateTime today = DateTime.Now;

                    TimeSpan difference = today - startDate;
                    var days = difference.TotalDays;

                    users.Add(new ViewUser()
                    {
                        Username = user.Username,
                        Rank = user.Rank,
                        Elo = user.Elo,
                        ProfilePic = user.ProfilePic,
                        Country = user.Country,
                        DaysAsMember = days
                    });
                }

                ViewBag.TopPlayers = users;
            }
            //Active state css ViewBags
            ViewBag.LeaderboardSelected = "activeNav";

            return View();
        }
    }
}