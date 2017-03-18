using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QueryMaster;
using QueryMaster.GameServer;
using ArenaStars.GameLogsService1Reference;
using ArenaStars.Models;

namespace ArenaStars.Controllers
{
    public class ServerController : Controller
    {
        string ServerControllerPath = @"put your path here or make it automatic";
        ArenaStarsContext db = new Models.ArenaStarsContext();

        // GET: Server
        public void ServerStart()
        {
            //Simulates 2 players matched from matchmaking function
            string LINUS = "LINUS";
            string Nicke = "LVL 8 MAGE";

            var playerA = from x in db.Users
                          where x.Username == LINUS
                          select x;

            var playerB = from x in db.Users
                          where x.Username == Nicke
                          select x;

            Models.User PA = playerA.FirstOrDefault();
            Models.User PB = playerB.FirstOrDefault();


            GameLogsService1Reference.User logUserB = new GameLogsService1Reference.User();
            GameLogsService1Reference.Game logGame = new GameLogsService1Reference.Game();
            GameLogsService1Reference.User logUserA = new GameLogsService1Reference.User();
            logGame.Type = 0;

            logUserA.Username = PA.Username;
            logUserA.SteamId = PA.SteamId;

            logUserB.Username = PB.Username;
            logUserB.SteamId = PB.SteamId;

            try
            {
                GameServiceClient client = new GameServiceClient();
                client.WhitelistPlayers(logUserA, logUserB);
                client.WaitForPlayers();
                client.DeleteLog();
                client.StartGame();
                client.ReadServerLogs();
                client.SaveStatsAndGame(logUserA, logUserB, logGame);
                client.Close();
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(ServerControllerPath, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace + Environment.NewLine + "Innerexception :" + ex.InnerException +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
                string playerAID = "\"" + logUserA.SteamId + "\"";
                string playerBID = "\"" + logUserB.SteamId + "\"";
                QueryMaster.GameServer.Server server = ServerQuery.GetServerInstance(EngineType.Source, "217.78.24.8", 28892);

                if (server.GetControl("lol"))
                {
                    server.Rcon.SendCommand("sm_whitelist_remove " + playerAID);
                    server.Rcon.SendCommand("sm_whitelist_remove " + playerBID);
                    server.Rcon.SendCommand("sm_kick @all");
                    server.Rcon.SendCommand("changelevel aim_map");
                    server.Rcon.SendCommand("warmup");
                }
            }

        }
    }
}