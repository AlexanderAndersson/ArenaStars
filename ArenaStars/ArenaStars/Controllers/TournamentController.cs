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

            var tournamentsParticipants = from t in context.Tournaments
                                          select t.Participants;

            var games = from g in context.Games
                        select g;

            var gamesP = from g in context.Games
                        select g.Participants;

            ViewBag.activeTournaments = tournaments;
            ViewBag.tParticipants = tournamentsParticipants;

            return View();
        }
    }
}