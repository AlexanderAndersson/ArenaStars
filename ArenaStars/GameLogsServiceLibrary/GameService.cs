using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ArenaStars.Models;
using System.IO;
using QueryMaster;
using QueryMaster.GameServer;
using System.Threading;
using System.Threading.Tasks;

namespace GameLogsServiceLibrary
{
    
    public class GameService : IGameService
    {
        string errorsPath = @"ftp://224021_master@ftp.arenastars.net/public_html/Errors.txt";
        
        string logsPath = @"ftp://224021_master@ftp.arenastars.net/public_html/Logs.txt";
        
        public void ReadServerLogs()
        {

            // Create textfile locally to save all logs comming from server
            using (StreamWriter writer = new StreamWriter(logsPath, true)) { 

                //Connect to gameserver
               QueryMaster.GameServer.Server server = ServerQuery.GetServerInstance(EngineType.Source, "217.78.24.8", 28892);
            //Get logs from server. To get these,
            //Type in server console: logaddress_add YOURIP:9871
            //Port 9871 is default for logs
            Logs logs = server.GetLogs(9871);

            //Start listen to logs
            var event1 = logs.GetEventsInstance();
            //Copy logs to your txt file
            event1.LogReceived += (o, e) => writer.Write(e.Message + Environment.NewLine);
            logs.Start();
                StopGame(event1, logs);
            logs.Stop();

            
            }
            

        }

        public void WhitelistPlayers(ArenaStars.Models.Game _game)
        {
           
            try
            {
                ArenaStarsContext db = new ArenaStarsContext();
                var findGame = from x in db.Games
                               where x.Id == _game.Id
                               select x;
                ArenaStars.Models.Game g = findGame.FirstOrDefault();

                User playerA = g.Participants.FirstOrDefault();
                User playerB = g.Participants.LastOrDefault();
                //Add players to the whitelist
                string playerAID = "\"" + playerA.SteamId + "\"";
                string playerBID = "\"" + playerB.SteamId + "\"";

                QueryMaster.GameServer.Server server = ServerQuery.GetServerInstance(EngineType.Source, "217.78.24.8", 28892);

                if (server.GetControl("lol"))
                {
                    server.Rcon.SendCommand("sm_whitelist_add " + playerAID);
                    server.Rcon.SendCommand("sm_whitelist_add " + playerBID);
                }
            }
            catch (Exception ex)
            {

                using (StreamWriter writer = new StreamWriter(errorsPath, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace + Environment.NewLine + "Innerexception :" + ex.InnerException +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
            }


        }


        public void StartGame()
        {
            QueryMaster.GameServer.Server server = ServerQuery.GetServerInstance(EngineType.Source, "217.78.24.8", 28892);
           if(server.GetControl("lol"))  
            server.Rcon.SendCommand("1on1");
            
        }

        public void DeleteLog()
        {
            //Temporary fix for file not overwriting. Cant have multiple games going if using this.
            if (File.Exists(logsPath))
            {
                File.Delete(logsPath);
            }
        }

        public void SaveStatsAndGame(ArenaStars.Models.Game _game)
        {
            try
            {

                using (ArenaStarsContext db = new ArenaStarsContext())
                {
                    
                    var findGame = from x in db.Games
                                   where x.Id == _game.Id
                                   select x;
                    ArenaStars.Models.Game g = findGame.FirstOrDefault();
                    User playerA = g.Participants.FirstOrDefault();
                    User playerB = g.Participants.LastOrDefault();

                    QueryMaster.GameServer.Server server = ServerQuery.GetServerInstance(EngineType.Source, "217.78.24.8", 28892);
                    ServerInfo info = server.GetInfo();


                    string playerAName = "\"" + playerA.Username;
                    string playerASteamID = playerA.SteamId;
                    int playerAKills = 0;
                    int playerADeaths = 0;
                    int playerAHSCount = 0;

                    string playerBName = "\"" + playerB.Username;
                    string playerBSteamID = playerB.SteamId;
                    int playerBKills = 0;
                    int playerBDeaths = 0;
                    int playerBHSCount = 0;

                    //Spagetthi for getting kills,deaths etc..
                    //Reads every line in Logs.txt and calculates
                    foreach (var line in File.ReadAllLines(logsPath))
                    {
                        if (line.StartsWith(playerAName) && line.Contains("killed"))
                        {
                            playerAKills++;
                            if (line.Contains("headshot"))
                            {
                                playerAHSCount++;
                            }
                        }
                        if (line.StartsWith(playerBName) && line.Contains("killed"))
                        {
                            playerBKills++;
                            if (line.Contains("headshot"))
                            {
                                playerBHSCount++;
                            }
                        }
                    }
                    playerADeaths = playerBKills;
                    playerBDeaths = playerAKills;


                    GameStats gameStatsA = new GameStats();
                    gameStatsA.SteamId = playerASteamID;
                    gameStatsA.Kills = playerAKills;
                    gameStatsA.Deaths = playerADeaths;
                    gameStatsA.HsRatio = headShotRatioConverter(playerAHSCount, playerAKills);
                    gameStatsA.Game = g;


                    GameStats gameStatsB = new GameStats();
                    gameStatsB.SteamId = playerBSteamID;
                    gameStatsB.Kills = playerBKills;
                    gameStatsB.Deaths = playerBDeaths;
                    gameStatsB.HsRatio = 0.33f;//headShotRatioConverter(playerBHSCount, playerBKills); //ISSUES
                    gameStatsB.Game = g;

                    g.Winner = getWinner(gameStatsA, gameStatsB, playerA, playerB, g);


                    db.GameStats.Add(gameStatsA);
                    db.GameStats.Add(gameStatsB);


                    db.SaveChanges();

                    //Match has finished so we remove players from the whitelist and restart map.
                    string playerAID = "\"" + playerA.SteamId + "\"";
                    string playerBID = "\"" + playerB.SteamId + "\"";


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
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(errorsPath, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace + Environment.NewLine + "Innerexception :" + ex.InnerException +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
            }

        }
      
        

        public double headShotRatioConverter(double numOfHS, double totalKills)
        {
            //Does not work for playerB at the moment. Database says it isnt returning float value.
            return Math.Round((numOfHS / totalKills) * 100);
        }

        public User getWinner(GameStats playerA, GameStats playerB, User userAID, User userBID, ArenaStars.Models.Game game)
        {
            //Player.Kills should be 16. Using less for testing purpose.
            //TODO:
            //Implement overtime score
            if (playerA.Kills == 2)
                return game.Winner = userAID;
            else
                return game.Winner = userBID;
        }

        void StopGame(LogEvents event1, Logs log)
        {
            //Here we listen for the team Score events.
            //When any team got 16 the game should be over and we can stop listening to logs and move on with code.
            //i should also be 16 here, using less for testing.
            //Very important to do thread.Sleep here else the program will crash due to memory overflow. 
            //Because of that we need to put the logs into a list so it can find the score we want to stop on.

            //TODO: Overtime score
            List<string> list = new List<string>();
            bool stop = false;
            while (!stop)
            {
                event1.TeamScoreReport += (o, e) => list.Add(e.Score);
                foreach (string i in list)
                {
                    if (i == "2")
                    {
                        Console.WriteLine("GAME OVER MF");
                        stop = true;
                        log.Stop();
                        // log.Dispose();
                    }
                }
                Thread.Sleep(1000);
            }
        }

        public void WaitForPlayers()
        {
            try
            {
                QueryMaster.GameServer.Server server = ServerQuery.GetServerInstance(EngineType.Source, "217.78.24.8", 28892);
                server.ReceiveTimeout = 200;
                bool stop = false;
                while (!stop)
                {
                    ServerInfo info = server.GetInfo();
                    if (info.Players == 2)
                    {
                        stop = true;
                    }
                    Thread.Sleep(10000);
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(errorsPath, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace + Environment.NewLine + "Innerexception :" + ex.InnerException +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }

            }
            

        }


    }
}
