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
            List<Dish> AllDishes = _context.Dishes
            .OrderByDescending(dish => dish.CreatedAt)
            .ToList();

            return View(AllDishes);
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
        [HttpPost("dish/create")]
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
        [HttpGet("dish/{dishId}")]
        public IActionResult DishInfo(int dishId)
        {
            Dish toRender = _context.Dishes
            .FirstOrDefault(dish => dish.DishId == dishId);
                
            return View(toRender);
        }


        //GET EDIT DiSH FORM
        [HttpGet("dish/edit/{dishId}")]
        public IActionResult EditDish(int dishId)
        {
            Dish toEdit = _context.Dishes
            .FirstOrDefault(dish => dish.DishId == dishId);

            if(toEdit == null)
            {
                return RedirectToAction("Index");
            }

            return View("EditDish", toEdit);
        }


        //UPDATE DISH WITH NEW INFO IN DB - POST
        [HttpPost("dish/update/{dishId}")]
        public IActionResult UpdateDish(int dishId, Dish fromForm)
        {
            if (ModelState.IsValid)
            {
                Dish inDb = _context.Dishes
                .FirstOrDefault(dish => dish.DishId == dishId);

                inDb.DishName = fromForm.DishName;
                inDb.ChefName = fromForm.ChefName;
                inDb.Tastiness = fromForm.Tastiness;
                inDb.Calories = fromForm.Calories;
                inDb.Description = fromForm.Description;
                inDb.UpdatedAt = DateTime.Now;

                _context.SaveChanges();

                return RedirectToAction("DishInfo", new { dishId = dishId });
            }
            else
            {
                return EditDish(dishId);
            }
        }


        //DELETE DISH FROM DB
        [HttpGet("dish/delete/{dishId}")]

        public RedirectToActionResult DeleteDishFromSession(int dishId)
        {
                Dish toDelete = _context.Dishes
                .FirstOrDefault(dish => dish.DishId == dishId);

                _context.Dishes.Remove(toDelete);
                _context.SaveChanges();
                
                return RedirectToAction("Index");
        }

    }
}