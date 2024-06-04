using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Models
{
    public class Image
    {
        public int ReferenceId { get; set; }
        public short Item { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ImageURL { get; set; }
    }
}
