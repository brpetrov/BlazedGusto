using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLibrary.Models
{
    public class Meal
    {
        [Required]
        public int MealId { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        [Required]
        public string Img { get; set; }
        [Required]
        public int PrepTime { get; set; }
        [Required]
        [StringLength(50)]
        public string Nationality { get; set; }
        [Required]
        public string Description { get; set; }
        public int IngredientsId { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        [Required]
        public int CookingInstructionsId { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Updated_At { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
    }
}
