using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CiteU.Models.Helper
{
    public static class ClaimCiteU
    {
        public static int getPoleFromClaim(IEnumerable<Claim> claims)
        {
            return Convert.ToInt16(claims.Where(w => w.Type == "Pole").FirstOrDefault().Value);
        }

        public static int getIdUserFromClaim(IEnumerable<Claim> claims)
        {
            return Convert.ToInt16(claims.Where(w => w.Type == "ID").FirstOrDefault().Value);
        }

        public static string getDroitFromClaim(IEnumerable<Claim> claims)
        {
            return claims.Where(w => w.Type == "Droit").FirstOrDefault().Value;
        }
        public const string Administrateur = "Administrateur";
        public const string Responsable = "Responsable";
        public const string Proclamateur = "Proclamateur";
    }
    
}
