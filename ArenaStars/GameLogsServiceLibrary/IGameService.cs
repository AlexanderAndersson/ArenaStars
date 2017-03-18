using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ArenaStars.Models;

namespace GameLogsServiceLibrary
{
   
    [ServiceContract]
    public interface IGameService
    {
        [OperationContract]
        void ReadServerLogs();
        [OperationContract]
        void StartGame();
        [OperationContract]
        void DeleteLog();
        [OperationContract]
        void SaveStatsAndGame(Game game);
        [OperationContract]
        void WhitelistPlayers(Game game);
        [OperationContract]
        void WaitForPlayers();

    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "GameLogsServiceLibrary.ContractType".
   
}
