﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArenaStars.Models
{
    public class Game
    {
        public long Id { get; set; }

        public TypeEnum Type { get; set; }

        public IList<User> Participants { get; set; }

        public virtual User Winner { get; set; }

        public string Map { get; set; }

        public IList<GameStats> GameStats { get; set; }

        public enum TypeEnum { Ranked, Unranked, Tournament, Challange }
    }
}