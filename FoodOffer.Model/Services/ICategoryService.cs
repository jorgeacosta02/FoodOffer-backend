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
        List<Category> GetCategories(short type);
        Category AddCategory(Category cat, short type);
        Category UpdateCategory(Category cat, short type);
        List<AttributeValue> GetAttibutesByCategory(short atc);
    }
}
