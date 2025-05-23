﻿using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Models.Order
{
    public class AddSpecialHatViewModel
    {
        [Required(ErrorMessage = "Please enter a name")]
        [MaxLength(100)]
        public string Name { get; set; }
        public int Size { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }

        [Required(ErrorMessage = "Please enter a price")]
        [Range(1, double.MaxValue, ErrorMessage = "The price must be 0 or higher")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Comment { get; set; }
        

        //[Required(ErrorMessage = "Please upload an image")]
        //public IFormFile Image { get; set; }

    }
}
