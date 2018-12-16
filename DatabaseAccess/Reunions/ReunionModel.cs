using DatabaseAccess.User;
using System;
using System.Collections.Generic;

namespace DatabaseAccess.Reunion
{
    public class ReunionModel
    {
        public int IdReunion { get; set; }
        public DateTime Date { get; set; }
        public DateTime Heure { get; set; }
        public int IdResponsable{ get; set; }
        public int IdCreateur { get; set; }
        public string Lieu { get; set; }
        public int IdPole { get; set; }
        public List<UserModel> Participants { get; set; }
    }
}
