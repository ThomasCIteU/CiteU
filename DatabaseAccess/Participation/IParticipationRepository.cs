using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.Participation
{
    public interface IParticipationRepository
    {
        void CreateParticipation(int idReunion, int idUser);
    }
}
