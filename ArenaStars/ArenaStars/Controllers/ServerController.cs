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
        string ServerControllerPath = @"ftp://224021_master@ftp.arenastars.net/public_html/Errors.txt";
        ArenaStarsContext db = new ArenaStarsContext();

        // GET: Server
        public void ServerStart()
        {
            long gameId = (long)TempData["transferGameId"];
            var getGame = from g in db.Games
                          where g.Id == gameId
                          select g;
            Models.Game ga = getGame.FirstOrDefault();
            Models.User playerA = ga.Participants.FirstOrDefault();
            Models.User playerB = ga.Participants.LastOrDefault();
            GameLogsService1Reference.Game logGame = new GameLogsService1Reference.Game();
            logGame.Id = ga.Id;

            try
            {
                GameServiceClient client = new GameServiceClient();
                client.WhitelistPlayers(logGame);
                client.WaitForPlayers();
                client.DeleteLog();
                client.StartGame();
                client.ReadServerLogs();
                client.SaveStatsAndGame(logGame);
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
                string playerAID = "\"" + playerA.SteamId + "\"";
                string playerBID = "\"" + playerB.SteamId + "\"";
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