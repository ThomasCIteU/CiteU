using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CiteU.Models;
using DatabaseAccess.User;
using CiteU.Models.User;
using DatabaseAccess.Assemblee;
using Microsoft.AspNetCore.Authorization;
using DatabaseAccess.Droit;

namespace CiteU.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public readonly IUserRepository _userRepository;
        public readonly IAssembleeRepository _assembleeRepository;
        public readonly IDroitRepository _droitRepository;
        public UserController (IUserRepository userRepository, IAssembleeRepository assembleeRepository, IDroitRepository droitRepository)
        {
            _userRepository = userRepository;
            _assembleeRepository = assembleeRepository;
            _droitRepository = droitRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var listUsers = new List<UserViewModel>();
            var list = _userRepository.GetUsers();
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

        [HttpGet]
        public IActionResult EditPage(int IdUser)
        {
            var vm = new UserEditViewModel()
            {
                CurrentUser = _userRepository.GetUser(IdUser),
                AllAssemblees = _assembleeRepository.GetAssemblees(),
                AllDroits = _droitRepository.getAllDroits(),
                IsCreation = false
            };
            return View("edit", vm);
        }

        [HttpGet]
        public IActionResult CreatePage()
        {
            var vm = new UserEditViewModel()
            {
                AllAssemblees = _assembleeRepository.GetAssemblees(),
                AllDroits = _droitRepository.getAllDroits(),
                IsCreation = true
            };
            return View("edit", vm);
        }

        [HttpPost]
        public IActionResult Create(UserEditViewModel User)
        {
            var user = User.CurrentUser;
            _userRepository.CreateUser(user.Nom, user.Prenom, user.Sexe, user.Mail, user.Phone, user.IdAssemblee, user.Privilege, user.Droit);

            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        //public IActionResult Edit(int IdUser, string Nom, string Prenom, char Sexe, string Mail, string Phone, string Assemblee, string Privilege)
        public IActionResult Edit(UserEditViewModel user)
        {
            var usr = user.CurrentUser;
            _userRepository.EditUser(usr.IdUser, usr.Nom, usr.Prenom, usr.Sexe, usr.Mail, usr.Phone, usr.IdAssemblee, usr.Privilege, usr.Droit);

            return RedirectToAction("Index", "User", usr.IdUser);
        }

        [HttpPost]
        public IActionResult Delete(int IdUser)
        {
            _userRepository.DeleteUser(IdUser);

            return RedirectToAction("Index", "User");
        }
    }
}
