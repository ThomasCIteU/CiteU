using CiteU.Models.Reunion;
using DatabaseAccess.Reunion;
using System;

namespace CiteU.Models.Planning
{
    public class DayViewModel
    {
        public DateTime Date { get; set; }
        public ReunionModel Reunion {get; set;}
    }
}