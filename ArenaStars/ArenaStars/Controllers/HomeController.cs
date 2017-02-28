using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArenaStars.Models;

namespace ArenaStars.Controllers
{
    public class HomeController : Controller
    {
        ArenaStarsContext context = new ArenaStarsContext();

        public ActionResult Index()
        {
            context.Database.Initialize(true);

            var rankedWinner = from g in context.Games
                               //where g.Type == Game.GameTypeEnum.Ranked
                               select g.Winner;

            var tWinners = from t in context.Tournaments
                           select t.Winner;

            ViewBag.TWinner = tWinners;
            ViewBag.Winner = rankedWinner;
      
            return View();
        }
        public ActionResult DummyData()
        {
            User u1 = new User() { Username = "Nicke" };
            User u2 = new User() { Username = "Alex" };
            context.Users.Add(u1);
            context.Users.Add(u2);
            context.SaveChanges();

            u1 = context.Users.ToList().FirstOrDefault();
            u2 = context.Users.ToList().LastOrDefault();

            Game newGame = new Game()
            {
                Participants = new List<User>()
                {
                    u1,
                    u2
                },
                Winner = u1,
                Map = "de_dust2",
                Type = Game.GameTypeEnum.Tournament
            };
            Game newGame2 = new Game()
            {
                Participants = new List<User>()
                {
                    u1,
                    u2
                },
                Winner = u2,
                Map = "aim_map",
                Type = Game.GameTypeEnum.Ranked
            };
            context.Games.Add(newGame);
            context.Games.Add(newGame2);
            context.SaveChanges();

            Tournament newTournament = new Tournament()
            {
                Name = "AllStars only m8",
                Participants = new List<User>()
                    {
                        u1,
                        u2
                    },
                Winner = u1,
                Games = new List<Game>()
                    {
                        context.Games.ToList().FirstOrDefault()
                    },
                Type = Tournament.TournamentTypeEnum.AllStars
            };
            context.Tournaments.Add(newTournament);
            context.SaveChanges();

            return RedirectToAction("/Index", "Home");
        }

    }
}