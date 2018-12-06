using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.Langue
{
    public interface ILangueRepository
    {
        List<LangueModel> GetLangues();
        LangueModel GetLangue(int idLangue);
        void EditLangue(int IdLangue, string Nom);
        void DeleteLangue(int IdLangue);
        void CreateLangue(string nom);
    }
}
