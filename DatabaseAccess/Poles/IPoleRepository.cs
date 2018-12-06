using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.Pole
{
    public interface IPoleRepository
    {
        List<PoleModel> GetPoles();
        PoleModel GetPole(int idPole);
        void EditPole(int IdPole, string Libelle, int idResponsable, int idAdjoint);
        void DeletePole(int IdPole);
    }
}
