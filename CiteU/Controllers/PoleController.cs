using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CiteU.Models;
using DatabaseAccess.Pole;
using CiteU.Models.Pole;
using DatabaseAccess.User;

namespace CiteU.Controllers
{
    public class PoleController : Controller
    {
        public readonly IPoleRepository _poleRepository;
        public readonly IUserRepository _userRepository;
        public PoleController(IPoleRepository PoleRepository, IUserRepository userRepository)
        {
            _poleRepository = PoleRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var listDesPoles = new List<PoleViewModel>();
            var list = _poleRepository.GetPoles();
            foreach(var pole in list)
            {
                listDesPoles.Add(new PoleViewModel()
                {
                    IdPole = pole.IdPole,
                    IdAdjoint = pole.IdAdjoint,
                    IdResponsable = pole.IdResponsable,
                    Libelle = pole.Libelle,
                    NomAdjoint = _userRepository.GetUser(pole.IdAdjoint).Nom + " " + _userRepository.GetUser(pole.IdAdjoint).Prenom,
                    NomResponsable = _userRepository.GetUser(pole.IdResponsable).Nom + " " + _userRepository.GetUser(pole.IdResponsable).Prenom,
                });
            }
            var vm = new PolesViewModel()
            {
                ListPoles = listDesPoles
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult EditPage(int IdPole)
        {
            var vm = new PoleEditViewModel()
            {
                CurrentPole = _poleRepository.GetPole(IdPole), 
                AllUsers = _userRepository.GetUsers(),
                IsCreation = false
            };
            return View("edit", vm);
        }

        [HttpPost]
        public IActionResult Edit(PoleEditViewModel Pole)
        {
            var pole = Pole.CurrentPole;
            _poleRepository.EditPole(pole.IdPole, pole.Libelle, pole.IdResponsable, pole.IdAdjoint);

            return RedirectToAction("Index", "Pole", pole.IdPole);
        }

        [HttpPost]
        public IActionResult CreatePage()
        {
            var vm = new PoleEditViewModel()
            {
                AllUsers = _userRepository.GetUsers(),
                IsCreation = true
            };
            return View("edit", vm);
        }

        [HttpPost]
        public IActionResult Create(PoleEditViewModel Pole)
        {
            var pole = Pole.CurrentPole;
            _poleRepository.CreatePole(pole.Libelle, pole.IdResponsable, pole.IdAdjoint);

            return RedirectToAction("Index", "Pole", pole.IdPole);
        }

        [HttpPost]
        public IActionResult Delete(int IdPole)
        {
            _poleRepository.DeletePole(IdPole);

            return RedirectToAction("Index", "Pole");
        }
    }
}
