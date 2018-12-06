using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CiteU.Models;
using DatabaseAccess.User;
using CiteU.Models.User;

namespace CiteU.Controllers
{
    public class UserController : Controller
    {
        public readonly IUserRepository _userRepository;
        public UserController (IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new UserViewModel()
            {
                ListUsers = _userRepository.GetUsers()
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult EditPage(int IdUser)
        {
            var vm = new UserEditViewModel()
            {
                CurrentUser = _userRepository.GetUser(IdUser)
            };
            return View("edit", vm);
        }

        [HttpPost]
        //public IActionResult Edit(int IdUser, string Nom, string Prenom, char Sexe, string Mail, string Phone, string Assemblee, string Privilege)
        public IActionResult Edit(UserEditViewModel user)
        {
            var usr = user.CurrentUser;
            _userRepository.EditUser(usr.IdUser, usr.Nom, usr.Prenom, usr.Sexe, usr.Mail, usr.Phone, usr.Assemblee, usr.Privilege);

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
