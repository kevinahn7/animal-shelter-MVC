using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;

namespace AnimalShelter.Controllers
{
    public class AnimalController : Controller
    {
        [HttpGet("/animals")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/animals")]
        public ActionResult Add(string name, string species, DateTime date, string gender)
        {
            Animals newAnimal = new Animals(name, species, date, gender);
            newAnimal.Save();

            return View("Index", Animals.GetAll());
        }
    }
}
