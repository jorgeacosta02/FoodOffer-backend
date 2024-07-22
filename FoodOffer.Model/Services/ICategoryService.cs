using FoodOffer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Services
{
    public interface ICategoryService
    {
        Category AddCategory(Category cat, short type);
        Category UpdateCategory(Category cat, short type);
    }
}
