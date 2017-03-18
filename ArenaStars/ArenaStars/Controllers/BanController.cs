using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArenaStars.Models;

namespace ArenaStars.Controllers
{
    public class BanController : Controller
    {
        public ActionResult Index()
        {
            //Active state css ViewBags
            ViewBag.BanlistSelected = "activeNav";
            List<User> getAllBannedUsers = new List<Models.User>();
            using (ArenaStarsContext context = new ArenaStarsContext())
            {
                getAllBannedUsers = (from u in context.Users
                                        where u.IsTerminated == true
                                        orderby u.BanFrom descending
                                        select u).ToList();


            }
                


            return View(getAllBannedUsers);
        }
    }
}