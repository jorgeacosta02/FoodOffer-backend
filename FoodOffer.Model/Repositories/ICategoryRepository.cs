using FoodOffer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Repositories
{
    public interface ICategoryRepository
    {
        short InsertCategory(Category category, short type);
        bool UpdateCategory(Category category, short type);
    }
}
