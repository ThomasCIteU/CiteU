using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.Assemblee
{
    public interface IAssembleeRepository
    {
        List<AssembleeModel> GetAssemblees();
        AssembleeModel GetAssemblee(int idAssemblee);
        void EditAssemblee(int IdAssemblee, string nom, int idPole);
        void DeleteAssemblee(int IdAssemblee);
        void CreateAssemblee(string nom, int idPole);
    }
}
