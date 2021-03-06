﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CiteU.Models;
using DatabaseAccess.Langue;
using CiteU.Models.Langue;
using DatabaseAccess.User;
using Microsoft.AspNetCore.Authorization;
using CiteU.Models.Helper;
using System.Security.Claims;

namespace CiteU.Controllers
{
    [Authorize]
    public class LangueController : Controller
    {
        public readonly ILangueRepository _LangueRepository;
        public LangueController(ILangueRepository LangueRepository)
        {
            _LangueRepository = LangueRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;

            int pole = ClaimCiteU.getPoleFromClaim(identity.Claims);
            string role = ClaimCiteU.getDroitFromClaim(identity.Claims);

            ViewData["CanEdit"] = (role == ClaimCiteU.Administrateur);
            var listDesLangues = new List<LangueViewModel>();
            var list = _LangueRepository.GetLangues();
            foreach(var Langue in list)
            {
                listDesLangues.Add(new LangueViewModel()
                {
                    IdLangue = Langue.IdLangue,
                    Nom = Langue.Nom
                });
            }
            var vm = new LanguesViewModel()
            {
                ListLangues = listDesLangues
            };
            return View(vm);
        }

        [HttpGet]
        [Authorize(Policy = ClaimCiteU.Administrateur)]
        public IActionResult EditPage(int IdLangue)
        {
            var vm = new LangueEditViewModel()
            {
                CurrentLangue = _LangueRepository.GetLangue(IdLangue), 
                IsCreation = false
            };
            return View("edit", vm);
        }

        [Authorize(Policy = ClaimCiteU.Administrateur)]
        [HttpPost]
        public IActionResult Edit(LangueEditViewModel Languevm)
        {
            var Langue = Languevm.CurrentLangue;
            _LangueRepository.EditLangue(Langue.IdLangue, Langue.Nom);

            return RedirectToAction("Index", "Langue", Langue.IdLangue);
        }

        [Authorize(Policy = ClaimCiteU.Administrateur)]
        [HttpGet]
        public IActionResult CreatePage()
        {
            var vm = new LangueEditViewModel()
            {
                IsCreation = true
            };
            return View("edit", vm);
        }

        [Authorize(Policy = ClaimCiteU.Administrateur)]
        [HttpPost]
        public IActionResult Create(LangueEditViewModel Languevm)
        {
            var Langue = Languevm.CurrentLangue;
            _LangueRepository.CreateLangue(Langue.Nom);

            return RedirectToAction("Index", "Langue", Langue.IdLangue);
        }

        [Authorize(Policy = ClaimCiteU.Administrateur)]
        [HttpPost]
        public IActionResult Delete(int IdLangue)
        {
            _LangueRepository.DeleteLangue(IdLangue);

            return RedirectToAction("Index", "Langue");
        }
    }
}
