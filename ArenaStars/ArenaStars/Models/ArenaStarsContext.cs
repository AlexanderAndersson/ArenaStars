using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ArenaStars.Models
{
    public class ArenaStarsContext : DbContext
    {
        public ArenaStarsContext() : base ("name=ASdb")
        {
            //this.Configuration.LazyLoadingEnabled = false;
        } 
    }
}