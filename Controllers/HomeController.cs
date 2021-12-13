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
            .OrderByDescending(dish => dish.CreatedAt);

            return View();
        }


        [HttpGet("return_home")]
        public RedirectToActionResult ReturnHome()
        {
            return RedirectToAction("Index");
        }


        //GET NEW DISH FORM 
        [HttpGet("new_dish")]
        public ViewResult NewDishForm()
        {
            return View("NewDish");
        }


        //CREATE DISH - POST TO DB & RETURN TO HOME PAGE (INDEX)
        [HttpPost("create")]
        public IActionResult NewDish(Dish fromForm)
        {
            if(ModelState.IsValid)
            {
                //Add Dish to the database
                _context.Add(fromForm);
                _context.SaveChanges();

                //Something cool we can do
                //Console.WriteLine(fromForm.DishId);

                return RedirectToAction ("Index");
            }
            else
            {
                return View("NewDish");
            }
        }


        //GET ONE DISH INFO VIA LINK REFERENCE
        [HttpGet("{dishId}")]
        public IActionResult DishInfo(int dishId)
        {
            Dish model = _context.Dishes
            .FirstOrDefault(dish => dish.DishId == dishId);

            if(model == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("DishInfo");
            }
        }

    }
}