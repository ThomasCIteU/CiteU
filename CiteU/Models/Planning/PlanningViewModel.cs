﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiteU.Models.Planning
{
    public class PlanningViewModel
    {
        public DateTime Actual { get; set; }
        public List<List<DayViewModel>> Weeks { get; set; }
    }
}
