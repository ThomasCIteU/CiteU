using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.Droit
{
    public interface IDroitRepository
    {
        DroitModel getDroit(int idDroit);
        List<DroitModel> getAllDroits();
    }
}
