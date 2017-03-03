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
            var activeTournaments = from t in context.Tournaments
                                    where t.HasEnded == false
                                    select t;

            ViewBag.activeTournaments = activeTournaments;

            return View();
        }
    }
}