using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.Reunion
{
    public interface IReunionRepository
    {
        List<ReunionModel> GetAllReunions();
        void CreateReunion(DateTime date, int idResponsable, int idCreateur, string Lieu, int idPole);
    }
}
