using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArenaStars.Models
{
    public class Server
    {
        public long Id { get; set; }
        
        public string Name { get; set; }

        public string IPaddress { get; set; }

        public bool isInUse { get; set; }
    }
}