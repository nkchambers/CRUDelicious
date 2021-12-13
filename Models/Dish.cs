using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }


        [Display(Name = "Name of Dish")]
        [Required(ErrorMessage ="Please provide a name for your new dish.")]
        [MinLength(1, ErrorMessage = "Your new dish's name must be at least 1 character long.")]
        public string DishName { get; set; }


        [Display(Name = "Chef's Name")]
        [Required(ErrorMessage ="Please provide a name for the Chef who created the dish.")]
        [MinLength(3, ErrorMessage = "Chef's name must be at least 3 characters long.")]
        public string ChefName { get; set; }


        [Display(Name = "Tastiness Level (Rate on Scale 1-5)")]
        [Required(ErrorMessage ="Please select a tastienss level (1-5).")]
        [Range(1, 5)]
        public int? Tastiness { get; set; }
                
        
        [Display(Name = "# of Calories")]
        [Required(ErrorMessage ="Calorie input must be greater than 0.")]
        public int? Calories { get; set; }


        [Display(Name = "Description")]
        [Required(ErrorMessage ="Please provide a description for your new dish.")]
        [MinLength(3, ErrorMessage = "Dish's description must be at least 3 characters long.")]
        public string Description { get; set; }

        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        public Dish() {}


        public Dish(string name, string chef, int tastiness, int calories, string description)
        {
            DishName = name;
            ChefName = chef;
            Tastiness = tastiness;
            Calories = calories;
            Description = description;
        }
    }
}