using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QueryMaster;
using QueryMaster.GameServer;
using ArenaStars.GameLogsServiceReference;
using ArenaStars.Models;

namespace ArenaStars.Controllers
{
    public class ServerController : Controller
    {
       ArenaStarsContext db = new Models.ArenaStarsContext();

        // GET: Server
        public void ServerStart()
        {
            //Simulates 2 players matched from matchmaking function
            string LINUS = "LVL 8 MAGE";
            string Nicke = "Nicke";

            var playerA = from x in db.Users
                          where x.Username == LINUS
                          select x;

            var playerB = from x in db.Users
                          where x.Username == Nicke
                          select x;

            Models.User PA = playerA.FirstOrDefault();
            Models.User PB = playerB.FirstOrDefault();


            GameLogsServiceReference.User logUserA = new GameLogsServiceReference.User();
            GameLogsServiceReference.User logUserB = new GameLogsServiceReference.User();
            GameLogsServiceReference.Game logGame = new GameLogsServiceReference.Game();
            logGame.Type = 0;

            logUserA.Username = PA.Username;
            logUserA.SteamId = PA.SteamId;

            logUserB.Username = PB.Username;
            logUserB.SteamId = PB.SteamId;


            GameServiceClient client = new GameServiceClient();
            client.WhitelistPlayers(logUserA, logUserB);
            client.DeleteLogAsync();
            client.StartGameAsync();
            client.ReadServerLogs();
            client.SaveStatsAndGame(logUserA, logUserB, logGame);
            client.Close();
        }
    }
}