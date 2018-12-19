using CiteU.Models.Planning;
using System;

namespace CiteU.Models.Planning
{
    public class DayViewModel
    {
        public DateTime Date { get; set; }
        public ReunionPlanningViewModel Reunion {get; set;}
    }
}