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
            string user = Session["username"].ToString();
            ViewBag.IsParticipating = false;

            var tournament = from t in context.Tournaments
                             where t.Id == id
                             select t;

            var parInTournamnet = from t in context.Tournaments
                                  where t.Id == id
                                  select t.Participants;

            var tPlayerLimit = from t in context.Tournaments
                               where t.Id == id
                               select t.PlayerLimit;

            Tournament newt = tournament.ToList().FirstOrDefault();

            foreach (User u in newt.Participants)
            {
                if (u.Username == user)
                {
                    ViewBag.IsParticipating = true;
                    break;
                }
            }

            ViewBag.PlayerLimit = tPlayerLimit.FirstOrDefault();
            ViewBag.Participants = parInTournamnet.FirstOrDefault().ToList();
            ViewBag.Tournament = tournament;

            //Active state css ViewBag
            ViewBag.TournamentSelected = "activeNav";

            return View();
        }

        public ActionResult JoinTournament(long pId)
        {
            string user = Session["username"].ToString();

            var tournament = from t in context.Tournaments
                             where t.Id == pId
                             select t;

            var loggedInUser = from u in context.Users
                               where u.Username == user
                               select u;

            Tournament updateTournament = tournament.ToList().FirstOrDefault();
            User currentUser = loggedInUser.ToList().FirstOrDefault();

            updateTournament.Participants.Add(currentUser);
            context.SaveChanges();

            var participantInfo = new
            {
                Username = currentUser.Username,
                Rank = currentUser.Rank.ToString(),
                Count = updateTournament.Participants.Count
            };

            return Json(new { info = participantInfo }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult LeaveTournament(long pId)
        {
            string user = Session["username"].ToString();

            var tournament = from t in context.Tournaments
                             where t.Id == pId
                             select t;

            var loggedInUser = from u in context.Users
                               where u.Username == user
                               select u;

            Tournament updateTournament = tournament.ToList().FirstOrDefault();
            User currentUser = loggedInUser.ToList().FirstOrDefault();

            updateTournament.Participants.Remove(currentUser);
            context.SaveChanges();


            return RedirectToAction("Index/Tournament");
        }
    }
}