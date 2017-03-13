using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArenaStars.Models;

namespace ArenaStars.Controllers
{
    public class AdminController : Controller
    {
        ArenaStarsContext context = new ArenaStarsContext();

        public ActionResult Tournaments()
        {
            var tournaments = from t in context.Tournaments
                              orderby t.StartDate
                              select t;

            ViewBag.Tournaments = tournaments;

            //Active state css ViewBags
            ViewBag.AdminTSelected = "activeNav";
            ViewBag.AdminSelected = "activeNav";

            return View();
        }

        public ActionResult AddTournament(string pName, DateTime? pStartDate, DateTime? pCheckIn, int pPlayerLimit, Tournament.TournamentTypeEnum pType, User.RankEnum pMinRank, User.RankEnum pMaxRank)
        {
            Tournament newTournament = new Tournament()
            {
                Name = pName,
                StartDate = pStartDate,
                CheckInDate = pCheckIn,
                PlayerLimit = pPlayerLimit,
                Type = pType,
                MinRank = pMinRank,
                MaxRank = pMaxRank,
                CreatedDate = DateTime.Now,
                TrophyPic = "~/Images/Trophy/Trophy1.png"
            };

            context.Tournaments.Add(newTournament);
            context.SaveChanges();

            var tournament = new
            {
                Name = newTournament.Name,
                StartDate = newTournament.StartDate,
                PlayerLimit = newTournament.PlayerLimit,
                Type = newTournament.Type.ToString(),
                MaxRank = newTournament.MaxRank.ToString(),
                MinRank = newTournament.MinRank.ToString(),
                //TrophyPic = newTournament.TrophyPic,
            };

            return Json(new { newT = tournament }, JsonRequestBehavior.DenyGet);

        }

        public ActionResult Users()
        {
            //Active state css ViewBags
            ViewBag.AdminUSelected = "activeNav";
            ViewBag.AdminSelected = "activeNav";

            return View();
        }

        public ActionResult Reports()
        {
            //Active state css ViewBags
            ViewBag.AdminRSelected = "activeNav";
            ViewBag.AdminSelected = "activeNav";

            return View();
        }
    }
}