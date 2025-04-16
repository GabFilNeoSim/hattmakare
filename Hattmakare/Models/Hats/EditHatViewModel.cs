﻿using Hattmakare.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Models.Hats
{
    public class EditHatViewModel
    {
        public Hat hat { get; set; }
       

        public int Hid { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        [MaxLength(100)]
        public string Name { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public int InStock { get; set; }

        //[Required(ErrorMessage = "Please enter a price")]
        //[Range(0, double.MaxValue, ErrorMessage = "The price must be 0 or higher")]
        //public decimal Price {  get; set; }

        [Required(ErrorMessage = "Please upload an image")]
        public IFormFile Image { get; set; }
    }
}
