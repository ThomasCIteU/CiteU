using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.Reunion
{
    public interface IReunionRepository
    {
        List<ReunionModel> GetAllReunions();
        void CreateReunion(DateTime date, int idResponsable, int idCreateur, string Lieu, int idPole);
        void DeleteReunion(int IdReunion);
        ReunionModel GetReunion(int idReunion);
        void EditReunion(int idReunion, DateTime date, int idResponsable, int idCreateur, string lieu, int idPole);
    }
}
