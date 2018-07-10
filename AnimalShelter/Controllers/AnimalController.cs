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
        public ActionResult Index()
        {
            List<Animals> animalList = new List<Animals>();
            animalList = Animals.GetAll();
            return View(animalList);
        }

        [HttpPost("/animals")]
        public ActionResult Add(string name, string species, DateTime date, string gender)
        {
            Animals newAnimal = new Animals(name, species, date, gender);
            newAnimal.Save();
            List<Animals> animalList = new List<Animals>();
            animalList = Animals.GetAll();

            return RedirectToAction("Index");
        }

        [HttpPost("/animals/delete/{id}")]
        public ActionResult DeleteSingleAnimal(int id)
        {
            Animals.DeleteSingle(id);
            return RedirectToAction("Index");
        }

        [HttpPost("/delete")]
        public ActionResult Delete()
        {
            Animals.DeleteAll();
            return RedirectToAction("Index");
        }
    }
}
