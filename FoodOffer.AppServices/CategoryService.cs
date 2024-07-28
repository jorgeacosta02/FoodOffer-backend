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
        private readonly IAttributeRepository _attributeRepository;
        public CategoryService(ICategoryRepository categoryRepository, IAttributeRepository attributeRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
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

        public List<AttributeValue> GetAttibutesByCategory(short atc)
        {
            return _attributeRepository.GetAttributesByCategory(atc);
        }

    }
}
