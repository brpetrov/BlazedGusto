using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLibrary.Models
{
    public class CookingInstruction
    {
        [Required]
        public int CookingInstructionId { get; set; }
        [Required]
        public string Img { get; set; }
        [Required]
        public int Step { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int MealId { get; set; }

    }
}
