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

            ViewBag.Tournaments = tournaments;

            //Active state css ViewBag
            ViewBag.TournamentSelected = "activeNav";

            return View();
        }

        public ActionResult TournamentInfo(long id)
        {
            var tournament = from t in context.Tournaments
                             where t.Id == id
                             select t;

            var parInTournamnet = from t in context.Tournaments
                                  where t.Id == id
                                  select t.Participants;

            ViewBag.Participants = parInTournamnet.FirstOrDefault().ToList();
            ViewBag.Tournament = tournament;

            //Active state css ViewBag
            ViewBag.TournamentSelected = "activeNav";

            return View();
        }

        public ActionResult JoinTournament()
        {
            

            return View();
        }
    }
}