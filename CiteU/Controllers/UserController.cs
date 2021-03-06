﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DatabaseAccess.User;
using CiteU.Models.User;
using DatabaseAccess.Assemblee;
using Microsoft.AspNetCore.Authorization;
using DatabaseAccess.Droit;
using CiteU.Models.Helper;
using System.Security.Claims;
using DatabaseAccess.Mail;
using DatabaseAccess.Langue;

namespace CiteU.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public readonly IUserRepository _userRepository;
        public readonly IAssembleeRepository _assembleeRepository;
        public readonly IDroitRepository _droitRepository;
        public readonly IMailRepository _mailRepository;
        public readonly ILangueRepository _langueRepository;
        public UserController (IUserRepository userRepository, IAssembleeRepository assembleeRepository, IDroitRepository droitRepository, IMailRepository mailRepository, ILangueRepository langueRepository)
        {
            _userRepository = userRepository;
            _assembleeRepository = assembleeRepository;
            _droitRepository = droitRepository;
            _mailRepository = mailRepository;
            _langueRepository = langueRepository;
        }
        public void SetViewData()
        {
            var identity = (ClaimsIdentity)User.Identity;
            string role = ClaimCiteU.getDroitFromClaim(identity.Claims);
            ViewData["CanEdit"] = (role == ClaimCiteU.Administrateur);
        }
        [HttpGet]
        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;

            int poleUser = ClaimCiteU.getPoleFromClaim(identity.Claims);
            string role = ClaimCiteU.getDroitFromClaim(identity.Claims);

            ViewData["CanEdit"] = (role == ClaimCiteU.Administrateur);

            var listUsers = new List<UserViewModel>();
            var list = new List<UserModel>();
            if (role == ClaimCiteU.Administrateur)
            {
                list = _userRepository.GetUsers();
            }
            else
            {
                list = _userRepository.GetUsersByPole(poleUser);
            }
            foreach (var user in list)
            {
                listUsers.Add(new UserViewModel()
                {
                    IdUser = user.IdUser,
                    Nom = user.Nom,
                    Prenom = user.Prenom,
                    Sexe = user.Sexe,
                    Mail = user.Mail,
                    Phone = user.Phone,
                    IdAssemblee = user.IdAssemblee,
                    NomAssemblee = _assembleeRepository.GetAssemblee(user.IdAssemblee)?.Nom,
                    Privilege = user.Privilege, 
                    Droit = _droitRepository.getDroit(user.Droit)?.Libelle
                });
            }
            var vm = new UsersViewModel()
            {
                ListUsers = listUsers
            };
            return View(vm);
        }

        [Authorize(Policy = ClaimCiteU.Proclamateur)]
        [HttpGet]
        public IActionResult EditPage(int IdUser)
        {
            SetViewData();
            var vm = new UserEditViewModel()
            {
                CurrentUser = _userRepository.GetUser(IdUser),
                AllAssemblees = _assembleeRepository.GetAssemblees(),
                AllDroits = _droitRepository.getAllDroits(),
                AllLangues = _langueRepository.GetLangues(),
                IsCreation = false
            };
            return View("edit", vm);
        }

        [Authorize(Policy = ClaimCiteU.Administrateur)]
        [HttpGet]
        public IActionResult CreatePage()
        {
            SetViewData();
            var vm = new UserEditViewModel()
            {
                AllAssemblees = _assembleeRepository.GetAssemblees(),
                AllDroits = _droitRepository.getAllDroits(),
                AllLangues = _langueRepository.GetLangues(),
                IsCreation = true
            };
            return View("edit", vm);
        }

        [Authorize(Policy = ClaimCiteU.Administrateur)]
        [HttpGet]
        public IActionResult Inviter()
        {
            var vm = new InvitationViewModel()
            {
                AllDroits = _droitRepository.getAllDroits()
            };
            return View(vm);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult InscriptionPage(string mailUser)
        {
            ViewData["CanEdit"] = false;
            var user = _userRepository.GetUser(mailUser);

            var vm = new UserEditViewModel()
            {
                CurrentUser = _userRepository.GetUser(mailUser),
                AllAssemblees = _assembleeRepository.GetAssemblees(),
                AllDroits = _droitRepository.getAllDroits(),
                AllLangues = _langueRepository.GetLangues(),
                IsCreation = false
            };
            return View("edit", vm);
        }

        [HttpPost]
        public IActionResult Create(UserEditViewModel User)
        {
            var user = User.CurrentUser;
            _userRepository.CreateUser(user.Nom, user.Prenom, user.Sexe, user.Mail, user.Phone, user.IdAssemblee, user.Privilege, user.Droit, user.Mdp, user.LanguesParlees);

            return RedirectToAction("Index", "User");
        }

        [Authorize(Policy = ClaimCiteU.Proclamateur)]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Edit(UserEditViewModel user)
        {
            var usr = user.CurrentUser;
            _userRepository.EditUser(usr.IdUser, usr.Nom, usr.Prenom, usr.Sexe, usr.Mail, usr.Phone, usr.IdAssemblee, usr.Privilege, usr.Droit, usr.Mdp, usr.LanguesParlees);

            return RedirectToAction("Index", "User", usr.IdUser);
        }

        [Authorize(Policy = ClaimCiteU.Administrateur)]
        [HttpPost]
        public IActionResult Delete(int IdUser)
        {
            _userRepository.DeleteUser(IdUser);

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult InscrireUser(InvitationViewModel user)
        {
            _userRepository.CreateUser("", "", 'M', user.MailUser, "", 0, "", user.DroitUser, "", null);
            _mailRepository.SendEmailInscription(user.MailUser);

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult SendMail()
        {
            _mailRepository.SendEmailTest();

            return RedirectToAction("Index", "User");
        }
    }
}
