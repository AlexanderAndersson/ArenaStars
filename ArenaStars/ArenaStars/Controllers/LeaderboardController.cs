using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArenaStars.Controllers
{
    public class LeaderboardController : Controller
    {
        public ActionResult Index()
        {
            //Active state css ViewBags
            ViewBag.LeaderboardSelected = "activeNav";

            return View();
        }
    }
}