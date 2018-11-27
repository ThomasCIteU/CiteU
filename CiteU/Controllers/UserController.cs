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
        [HttpGet]
        public IActionResult Index()
        {
            var e = new UserRepository();
            var vm = new UserViewModel()
            {
                ListUsers = e.GetUsers()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(int IdUser)
        {
            var e = new UserRepository();
            var vm = new UserEditViewModel()
            {
                CurrentUser = e.GetUser(IdUser)
            };
            return View("edit", vm);
        }

        [HttpPost]
        //public IActionResult Edit(int IdUser, string Nom, string Prenom, char Sexe, string Mail, string Phone, string Assemblee, string Privilege)
        public IActionResult Edit(UserEditViewModel user)
        {
            var e = new UserRepository();
            var usr = user.CurrentUser;
            e.EditUser(usr.IdUser, usr.Nom, usr.Prenom, usr.Sexe, usr.Mail, usr.Phone, usr.Assemblee, usr.Privilege);

            var vm = new UserEditViewModel()
            {
                CurrentUser = e.GetUser(usr.IdUser)
            };
            return View("get", vm);
        }
    }
}
