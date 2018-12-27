using DatabaseAccess.User;
using System;
using System.Collections.Generic;

namespace CiteU.Models.Planning
{
    public class ReunionPlanningViewModel
    {
        public int IdReunion { get; set; }
        public DateTime Date { get; set; }
        public UserModel Responsable { get; set; }
        public string Lieu { get; set; }
        public int IdPole { get; set; }
        public List<UserModel> Participants{get;set;}
        public bool CurrentUserCanParticipe { get; set; }
    }
}