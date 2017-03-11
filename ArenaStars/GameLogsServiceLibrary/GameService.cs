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

namespace GameLogsServiceLibrary
{
    
    public class GameService : IGameService
    {
        string filePath = @"F:\Dokument\Visual Studio 2015\Projects\ArenaStars\ArenaStars\Error";
        string logsPath = @"F:\Dokument\Visual Studio 2015\Projects\ArenaStars\ArenaStars\Logs.txt";
        
        public void ReadServerLogs()
        {

            // Create textfile locally to save all logs comming from server
            using (StreamWriter writer = new StreamWriter(logsPath, true)) { 

                //Connect to gameserver
                Server server = ServerQuery.GetServerInstance(EngineType.Source, "217.78.24.8", 28892);
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

        public void StartGame()
        {
            Server server = ServerQuery.GetServerInstance(EngineType.Source, "217.78.24.8", 28892);
           if(server.GetControl("lol"))  
            server.Rcon.SendCommand("1on1");
            
        }

        public void DeleteLog()
        {
            //Temporary fix for file not overwriting. Cant have multiple games going if using this.
            if (File.Exists(@"F:\Dokument\Visual Studio 2015\Projects\ArenaStars\ArenaStars\Logs.txt"))
            {
                File.Delete(@"F:\Dokument\Visual Studio 2015\Projects\ArenaStars\ArenaStars\Logs.txt");
            }
        }

        public void SaveStatsAndGame(User _playerA, User _playerB, ArenaStars.Models.Game _gameType)
        {
           
            try
            {
                using (ArenaStarsContext db = new ArenaStarsContext())
                {
                    var findUserA = from x in db.Users
                                    where x.Username == _playerA.Username
                                    select x;
                    var findUserB = from x in db.Users
                                    where x.Username == _playerB.Username
                                    select x;

                    User playerA = findUserA.FirstOrDefault();
                    User playerB = findUserB.FirstOrDefault();

                    Server server = ServerQuery.GetServerInstance(EngineType.Source, "217.78.24.8", 28892);
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
                foreach (var line in File.ReadAllLines(@"F:\Dokument\Visual Studio 2015\Projects\ArenaStars\ArenaStars\Logs.txt"))
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


                    ArenaStars.Models.Game game = new ArenaStars.Models.Game();
                    game.Map = info.Map;
                    game.Type = _gameType.Type;

                    playerA.Games.Add(game);
                    playerB.Games.Add(game);
                 
                    GameStats gameStatsA = new GameStats();
                    gameStatsA.SteamId = playerASteamID;
                    gameStatsA.Kills = playerAKills;
                    gameStatsA.Deaths = playerADeaths;
                    gameStatsA.HsRatio = headShotRatioConverter(playerAHSCount, playerAKills);
                    gameStatsA.Game = game;
                  

                    GameStats gameStatsB = new GameStats();
                    gameStatsB.SteamId = playerBSteamID;
                    gameStatsB.Kills = playerBKills;
                    gameStatsB.Deaths = playerBDeaths;
                    gameStatsB.HsRatio = 0.33f;//headShotRatioConverter(playerBHSCount, playerBKills); //ISSUES
                    gameStatsB.Game = game;

                    game.Winner = getWinner(gameStatsA, gameStatsB, playerA, playerB, game);
                    db.Games.Add(game);

                    db.GameStats.Add(gameStatsA);
                    db.GameStats.Add(gameStatsB);


                    db.SaveChanges();

                }

            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
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


    }
}
