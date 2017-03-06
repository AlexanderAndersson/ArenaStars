using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArenaStars.Models;

namespace ArenaStars.Controllers
{
    public class TournamentController : Controller
    {
        ArenaStarsContext context = new ArenaStarsContext();
        public ActionResult Index()
        {
            var tournaments = from t in context.Tournaments
                                    select t;

            ViewBag.activeTournaments = tournaments;

            //Active state css ViewBag
            ViewBag.TournamentSelected = "activeNav";


            return View();
        }
    }
}