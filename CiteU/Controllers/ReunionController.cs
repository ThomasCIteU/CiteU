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
using CiteU.Models.Helper;
using System.Security.Claims;

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
            var identity = (ClaimsIdentity)User.Identity;

            int pole = ClaimCiteU.getPoleFromClaim(identity.Claims);
            string role = ClaimCiteU.getDroitFromClaim(identity.Claims);
            ViewData["CanEdit"] = (role != ClaimCiteU.Proclamateur);

            var listDesReunions = new List<ReunionViewModel>();
            var list = new List<ReunionModel>();

            if (role == ClaimCiteU.Administrateur)
            {
                list = _ReunionRepository.GetAllReunions();
            }
            else
            {
                list = _ReunionRepository.GetAllReunions(pole);
            }
            foreach (var Reunion in list)
            {
                var responsable = _userRepository.GetUser(Reunion.IdResponsable);
                var createur = _userRepository.GetUser(Reunion.IdCreateur);
                listDesReunions.Add(new ReunionViewModel()
                {
                    IdReunion = Reunion.IdReunion,
                    Date = Reunion.Date,
                    Responsable = responsable.Nom + " " + responsable.Prenom,
                    Createur = createur.Nom + " " + createur.Prenom,
                    Lieu = Reunion.Lieu,
                    Pole = _poleRepository.GetPole(Reunion.IdPole).Libelle
                });
            }
            var vm = new ReunionsViewModel()
            {
                ListReunions = listDesReunions.OrderByDescending(r => r.Date).ToList()
            };
            return View(vm);
        }

        [Authorize(Policy = ClaimCiteU.Administrateur)]
        [HttpGet]
        public IActionResult EditPage(int IdReunion)
        {
            var vm = new ReunionEditViewModel()
            {
                CurrentReunion = _ReunionRepository.GetReunion(IdReunion),
                AllPoles = _poleRepository.GetPoles(),
                AllUsers = _userRepository.GetUsers(),
                IsCreation = false
            };
            return View("edit", vm);
        }

        [Authorize(Policy = ClaimCiteU.Administrateur)]
        [HttpPost]
        public IActionResult Edit(ReunionEditViewModel reunion)
        {
            var Reunion = reunion.CurrentReunion;
            var date = reunion.CurrentReunion.Date.AddHours(reunion.CurrentReunion.Heure.Hour);
            date = date.AddMinutes(reunion.CurrentReunion.Heure.Minute);
            _ReunionRepository.EditReunion(Reunion.IdReunion, date, Reunion.IdResponsable, Reunion.IdCreateur, Reunion.Lieu, Reunion.IdPole);

            return RedirectToAction("Index", "Reunion", Reunion.IdReunion);
        }

        [Authorize(Policy = ClaimCiteU.Administrateur)]
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

        [Authorize(Policy = ClaimCiteU.Administrateur)]
        [HttpGet]
        public IActionResult CreatePageFromDate(DateTime Date)
        {
            var vm = new ReunionEditViewModel()
            {
                CurrentReunion = new ReunionModel
                {
                    Date = Date
                },
                AllUsers = _userRepository.GetUsers(),
                AllPoles = _poleRepository.GetPoles(),
                IsCreation = true
            };
            return View("edit", vm);
        }

        [Authorize(Policy = ClaimCiteU.Administrateur)]
        [HttpPost]
        public IActionResult Create(ReunionEditViewModel reunion)
        {
            var Reunion = reunion.CurrentReunion;
            var date = reunion.CurrentReunion.Date.AddHours(reunion.CurrentReunion.Heure.Hour);
            date = date.AddMinutes(reunion.CurrentReunion.Heure.Minute);
            _ReunionRepository.CreateReunion(date, Reunion.IdResponsable, Reunion.IdCreateur, Reunion.Lieu, Reunion.IdPole);

            return RedirectToAction("Index", "Reunion", Reunion.IdReunion);
        }

        [Authorize(Policy = ClaimCiteU.Administrateur)]
        [HttpPost]
        public IActionResult Delete(int IdReunion)
        {
            _ReunionRepository.DeleteReunion(IdReunion);

            return RedirectToAction("Index", "Reunion");
        }
    }
}
