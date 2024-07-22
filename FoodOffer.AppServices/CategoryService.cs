using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using FoodOffer.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.AppServices
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public List<Category> GetCategories(short type)
        {
            return _categoryRepository.GetCategories(type);
        }

        public Category AddCategory(Category cat, short type)
        {
            cat.Code = _categoryRepository.InsertCategory(cat, type);

            if (cat.Code == 0)
               throw new Exception("Error adding category");

            return cat;
        }

        public Category UpdateCategory(Category cat, short type)
        {
            if(!_categoryRepository.UpdateCategory(cat, type))
                throw new Exception("Error updating category");

            return cat;
        }

    }
}
