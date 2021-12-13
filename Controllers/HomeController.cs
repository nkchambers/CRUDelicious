using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;
using Microsoft.AspNetCore.Http; // This is where session comes from
using System.Linq;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;


        }


        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.AllDishes = _context.Dishes
            .ToArray();

            return View();
        }


        [HttpGet("return_home")]
        public RedirectToActionResult ReturnHome()
        {
            return RedirectToAction("Index");
        }


        [HttpGet("new_dish")]
        public ViewResult NewDishForm()
        {
            return View("NewDish");
        }


        [HttpPost("create")]
        public IActionResult NewDish(Dish fromForm)
        {
            if(ModelState.IsValid)
            {
                //Add Dish to the database
                _context.Add(fromForm);
                _context.SaveChanges();

                //Something cool we can do
                Console.WriteLine(fromForm.DishId);


                /*HttpContext.Session.SetString("ChefName", fromForm.ChefName);
                HttpContext.Session.SetString("DishName", fromForm.DishName);
                HttpContext.Session.SetInt32("Calories", (int)fromForm.Calories);
                HttpContext.Session.SetInt32("Tastiness", (int)fromForm.Tastiness);
                HttpContext.Session.SetString("Description", fromForm.Description);*/

                return RedirectToAction ("Index");
            }
            else
            {
                return View("NewDish");
            }
        }


        [HttpGet("info/{dishId}")]
        public IActionResult DishInfo(int dishId)
        {
            Dish DishToRender = _context.Dishes
            .FirstOrDefault(dish => dish.DishId == dishId);

            return View(DishToRender);
        }

    }
}