using System.ComponentModel.DataAnnotations;

namespace DatabaseAccess.Langue
{
    public class LangueModel
    {
        public int IdLangue { get; set; }
        [Required]
        public string Nom { get; set; }
    }
}
