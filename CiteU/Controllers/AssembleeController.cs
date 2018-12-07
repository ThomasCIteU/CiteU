using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CiteU.Models;
using DatabaseAccess.Assemblee;
using CiteU.Models.Assemblee;
using DatabaseAccess.Pole;
using Microsoft.AspNetCore.Authorization;

namespace CiteU.Controllers
{
    [Authorize]
    public class AssembleeController : Controller
    {
        public readonly IAssembleeRepository _assembleeRepository;
        public readonly IPoleRepository _poleRepository;
        public AssembleeController(IAssembleeRepository AssembleeRepository, IPoleRepository poleRepository)
        {
            _assembleeRepository = AssembleeRepository;
            _poleRepository = poleRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var listDesAssemblees = new List<AssembleeViewModel>();
            var list = _assembleeRepository.GetAssemblees();
            foreach (var Assemblee in list)
            {
                listDesAssemblees.Add(new AssembleeViewModel()
                {
                    IdAssemblee = Assemblee.IdAssemblee,
                    Nom = Assemblee.Nom,
                    IdPole = Assemblee.IdPole,
                    NomPole = _poleRepository.GetPole(Assemblee.IdPole).Libelle
                });
            }
            var vm = new AssembleesViewModel()
            {
                ListAssemblees = listDesAssemblees
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult EditPage(int IdAssemblee)
        {
            var vm = new AssembleeEditViewModel()
            {
                CurrentAssemblee = _assembleeRepository.GetAssemblee(IdAssemblee),
                AllPoles = _poleRepository.GetPoles(),
                IsCreation = false
            };
            return View("edit", vm);
        }

        [HttpPost]
        public IActionResult Edit(AssembleeEditViewModel assemblee)
        {
            var Assemblee = assemblee.CurrentAssemblee;
            _assembleeRepository.EditAssemblee(Assemblee.IdAssemblee, Assemblee.Nom, Assemblee.IdPole);

            return RedirectToAction("Index", "Assemblee", Assemblee.IdAssemblee);
        }

        [HttpGet]
        public IActionResult CreatePage()
        {
            var vm = new AssembleeEditViewModel()
            {
                AllPoles = _poleRepository.GetPoles(),
                IsCreation = true
            };
            return View("edit", vm);
        }

        [HttpPost]
        public IActionResult Create(AssembleeEditViewModel assemblee)
        {
            var Assemblee = assemblee.CurrentAssemblee;
            _assembleeRepository.CreateAssemblee(Assemblee.Nom, Assemblee.IdPole);

            return RedirectToAction("Index", "Assemblee", Assemblee.IdAssemblee);
        }

        [HttpPost]
        public IActionResult Delete(int IdAssemblee)
        {
            _assembleeRepository.DeleteAssemblee(IdAssemblee);

            return RedirectToAction("Index", "Assemblee");
        }
    }
}
