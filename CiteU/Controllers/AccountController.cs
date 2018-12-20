using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CiteU.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using DatabaseAccess.Login;
using DatabaseAccess.User;
using Microsoft.AspNetCore.Authorization;
using CiteU.Models.Login;
using Microsoft.AspNetCore.Http;
using CiteU.Models.Helper;
using DatabaseAccess.Pole;

namespace CiteU.Controllers
{
    public class AccountController : Controller
    {
        public readonly ILoginRepository _loginRepository;
        public readonly IPoleRepository _poleRepository;
        public AccountController(ILoginRepository loginRepository, IPoleRepository poleRepository)
        {
            _loginRepository = loginRepository;
            _poleRepository = poleRepository;
        }

        [AllowAnonymous]
        [Route("/Account/Login")]
        [HttpGet]
        public IActionResult Index(string message = "")
        {
            return PartialView("Index", new LoginViewModel(message));
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(string Mail, string Mdp)
        {
            var user = _loginRepository.Login(Mail, Mdp);
            if(user.IdUser == 0)
            {
                return RedirectToAction("Index", "Account", new { message = "Informations de connexion erronées." });
            }
            LogUser(user);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Account");
        }

        private async void LogUser(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nom),
                new Claim("FullName", user.Prenom),
                new Claim("Pole", _poleRepository.GetPoleFromUser(user.IdUser).ToString())

            };
            switch (user.Droit)
            {
                case 1:
                    claims.Add(new Claim("Droit", ClaimCiteU.Proclamateur));
                    break;

                case 2:
                    claims.Add(new Claim("Droit", ClaimCiteU.Responsable));
                    break;

                case 3:
                    claims.Add(new Claim("Droit", ClaimCiteU.Administrateur));
                    break;
            }

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. Required when setting the 
                // ExpireTimeSpan option of CookieAuthenticationOptions 
                // set with AddCookie. Also required when setting 
                // ExpiresUtc.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
