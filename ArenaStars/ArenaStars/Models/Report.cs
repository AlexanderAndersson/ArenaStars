﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArenaStars.Models
{
    public class Report
    {
        public long Id { get; set; }

        public ReasonEnum Reason { get; set; }

        public string Message { get; set; }

        public virtual User User { get; set; }

        public enum ReasonEnum { Cheating, Toxic, Griefing, Harassment, Other }
    }
}