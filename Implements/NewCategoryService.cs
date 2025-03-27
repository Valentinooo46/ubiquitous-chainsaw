using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OLX.Entities;
using OLX.Interfaces;
namespace OLX.Implements
{
    public class NewCategoryService : INewCategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        public NewCategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void CreateCategory(string name, string slug, ICollection<Subcategory> subcategories)
        {
            var category = new Category
            {
                Name = name,
                Slug = slug,
                Subcategories = subcategories
            };
            _categoryRepository.Add(category);
            _categoryRepository.SaveChanges();
        }
        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll().ToList();
        }
        public Category? GetCategoryById(int id)
        {
            return _categoryRepository.GetById(id);
        }
        public bool UpdateCategory(int id, string name)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return false;
            }
            category.Name = name;
            _categoryRepository.Update(category);
            _categoryRepository.SaveChanges();
            return true;
        }
        public bool DeleteCategory(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return false;
            }
            _categoryRepository.Delete(category);
            _categoryRepository.SaveChanges();
            return true;
        }
    }
}
