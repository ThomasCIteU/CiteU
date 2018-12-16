using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CiteU.Models;
using DatabaseAccess.Reunion;
using CiteU.Models.Reunion;
using DatabaseAccess.User;
using Microsoft.AspNetCore.Authorization;
using DatabaseAccess.Pole;

namespace CiteU.Controllers
{
    [Authorize]
    public class ReunionController : Controller
    {
        public readonly IReunionRepository _ReunionRepository;
        public readonly IUserRepository _userRepository;
        public readonly IPoleRepository _poleRepository;
        public ReunionController(IReunionRepository ReunionRepository, IUserRepository userRepository, IPoleRepository poleRepository)
        {
            _ReunionRepository = ReunionRepository;
            _userRepository = userRepository;
            _poleRepository = poleRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var listDesReunions = new List<ReunionViewModel>();
            var list = _ReunionRepository.GetAllReunions();
            foreach(var Reunion in list)
            {
                listDesReunions.Add(new ReunionViewModel()
                {
                    IdReunion = Reunion.IdReunion,
                    Date = Reunion.Date,
                    IdResponsable = Reunion.IdResponsable,
                    IdCreateur = Reunion.IdCreateur,
                    Lieu = Reunion.Lieu
                });
            }
            var vm = new ReunionsViewModel()
            {
                ListReunions = listDesReunions
            };
            return View(vm);
        }

        //[HttpGet]
        //public IActionResult EditPage(int IdReunion)
        //{
        //    var vm = new ReunionEditViewModel()
        //    {
        //        CurrentReunion = _ReunionRepository.GetReunion(IdReunion), 
        //        AllUsers = _userRepository.GetUsers(),
        //        IsCreation = false
        //    };
        //    return View("edit", vm);
        //}

        //[HttpPost]
        //public IActionResult Edit(ReunionEditViewModel Reunion)
        //{
        //    var Reunion = Reunion.CurrentReunion;
        //    _ReunionRepository.EditReunion(Reunion.IdReunion, Reunion.Libelle, Reunion.IdResponsable, Reunion.IdAdjoint);

        //    return RedirectToAction("Index", "Reunion", Reunion.IdReunion);
        //}

        [HttpGet]
        public IActionResult CreatePage()
        {
            var vm = new ReunionEditViewModel()
            {
                AllUsers = _userRepository.GetUsers(),
                AllPoles = _poleRepository.GetPoles(),
                IsCreation = true
            };
            return View("edit", vm);
        }

        [HttpPost]
        public IActionResult Create(ReunionEditViewModel reunion)
        {
            var Reunion = reunion.CurrentReunion;
            var date = reunion.CurrentReunion.Date.AddHours(reunion.CurrentReunion.Heure.Hour);
            date = date.AddMinutes(reunion.CurrentReunion.Heure.Minute);
            _ReunionRepository.CreateReunion(date, Reunion.IdResponsable, Reunion.IdCreateur, Reunion.Lieu, Reunion.IdPole);

            return RedirectToAction("Index", "Reunion", Reunion.IdReunion);
        }

        //[HttpPost]
        //public IActionResult Delete(int IdReunion)
        //{
        //    _ReunionRepository.DeleteReunion(IdReunion);

        //    return RedirectToAction("Index", "Reunion");
        //}
    }
}
