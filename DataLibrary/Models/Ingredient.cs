using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLibrary.Models
{
    public class Ingredient
    {
        [Required]
        public int IngredientId { get; set; }
        [Required]
        public string Img { get; set; }
        [Required]
        public double Quantity { get; set; }
        [Required]
        [StringLength(250)]
        public string Type { get; set; }
        [Required]
        public int MealId { get; set; }
        public Meal Meal { get; set; }

    }
}
