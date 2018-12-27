using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CiteU.Models;
using CiteU.Models.Planning;
using DatabaseAccess.Reunion;
using DatabaseAccess.User;
using DatabaseAccess.Pole;
using CiteU.Models.Helper;
using System.Security.Claims;
using DatabaseAccess.Participation;
using Microsoft.AspNetCore.Authorization;

namespace CiteU.Controllers
{
    public class PlanningController : Controller
    {
        public readonly IReunionRepository _ReunionRepository;
        public readonly IUserRepository _userRepository;
        public readonly IPoleRepository _poleRepository;
        public readonly IParticipationRepository _participationRepository;
        public PlanningController(IReunionRepository ReunionRepository, IUserRepository userRepository, IPoleRepository poleRepository, IParticipationRepository participationRepository)
        {
            _ReunionRepository = ReunionRepository;
            _userRepository = userRepository;
            _poleRepository = poleRepository;
            _participationRepository = participationRepository;
        }

        public IActionResult Index(int year = 2018, int month = 12)
        {
            var identity = (ClaimsIdentity)User.Identity;

            int pole = ClaimCiteU.getPoleFromClaim(identity.Claims);
            string role = ClaimCiteU.getDroitFromClaim(identity.Claims);
            int idUser = ClaimCiteU.getIdUserFromClaim(identity.Claims);
            ViewData["CanEdit"] = (role != ClaimCiteU.Proclamateur);
            var listMonthDays = new List<List<DayViewModel>>();
            int days = DateTime.DaysInMonth(year, month);
            var listWeekDays = new List<DayViewModel>();
            for (int day = 1; day <= days; day++)
            {
                var aDay = new DateTime(year, month, day);
                
                if(aDay.DayOfWeek == DayOfWeek.Monday || aDay.DayOfWeek == DayOfWeek.Tuesday || aDay.DayOfWeek == DayOfWeek.Wednesday)
                {
                    if(aDay.DayOfWeek == DayOfWeek.Monday)
                    {
                        listMonthDays.Add(listWeekDays.ToList());
                        listWeekDays = new List<DayViewModel>();
                    }
                    var reunion = _ReunionRepository.GetReunion(aDay, pole);
                    if (reunion != null)
                    {
                        var participants = _participationRepository.GetUsersByReunion(reunion.IdReunion);
                        var canParticipe = !(participants.Where(w => w.IdUser == idUser).Any());
                        listWeekDays.Add(new DayViewModel
                        {
                            Date = aDay,
                            Reunion = new ReunionPlanningViewModel()
                            {
                                IdReunion = reunion.IdReunion,
                                Date = reunion.Date,
                                Responsable = _userRepository.GetUser(reunion.IdResponsable),
                                Lieu = reunion.Lieu,
                                IdPole = reunion.IdPole,
                                Participants = participants,
                                CurrentUserCanParticipe = canParticipe
                            }
                        });
                    }
                    else
                    {
                        listWeekDays.Add(new DayViewModel
                        {
                            Date = aDay
                        });
                    }
                }
            }
            listMonthDays.Add(listWeekDays);
            var vm = new PlanningViewModel
            {
                Weeks = listMonthDays,
                Actual = new DateTime(year, month, 1)
            };
            return View(vm);
        }

        [Authorize(Policy = ClaimCiteU.Proclamateur)]
        [HttpPost]
        public IActionResult Participer(int IdReunion)
        {
            var identity = (ClaimsIdentity)User.Identity;
            int idUser = ClaimCiteU.getIdUserFromClaim(identity.Claims);
            _participationRepository.CreateParticipation(IdReunion, idUser);
            return RedirectToAction("Index", "Planning");
        }
    }
}
