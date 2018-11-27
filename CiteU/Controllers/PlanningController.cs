using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CiteU.Models;

namespace CiteU.Controllers
{
    public class PlanningController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
