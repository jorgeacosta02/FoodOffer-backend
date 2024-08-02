using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Models
{
    public class AppImage
    {
        public int ReferenceId { get; set; }
        public short Item { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public IFormFile ImageFile { get; set; }

        public AppImage() { }

        public AppImage(int referenceId, short item, string name, string path, IFormFile? imageFile)
        {
            ReferenceId = referenceId;
            Item = item;
            Name = name;
            Path = path;
            if(imageFile != null)
            {
                ImageFile = imageFile;
            }

        }
    }
}
