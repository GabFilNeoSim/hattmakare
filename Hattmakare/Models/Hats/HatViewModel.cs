using System.ComponentModel.DataAnnotations;
using Hattmakare.Data;
using Hattmakare.Data.Entities;

namespace Hattmakare.Models.Hats
{
    public class HatViewModel
    {
        public Hat hat { get; set; }
        public int Hid { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
