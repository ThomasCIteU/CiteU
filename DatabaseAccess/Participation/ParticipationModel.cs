using DatabaseAccess.User;
using System;
using System.Collections.Generic;

namespace DatabaseAccess.Participation
{
    public class ParticipationModel
    {
        public int IdParticipation { get; set; }
        public int IdReunion{ get; set; }
        public int IdUser { get; set; }
    }
}
