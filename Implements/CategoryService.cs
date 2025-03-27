using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLX.Implements
{
    public class CategoryService : Interfaces.ICategoryService
    {
        private readonly Interfaces.IRepository<Entities.CategoryEntity> _categoryRepository;
        public CategoryService(Interfaces.IRepository<Entities.CategoryEntity> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void CreateCategory(string name, string Description, int ParentId = 0)
        {
            var category = new Entities.CategoryEntity
            {
                Name = name,
                Description = Description,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now)
            };
            if (ParentId != 0 && _categoryRepository.GetQuery().Any(x => x.Id == ParentId))
            {
                category.ParentId = ParentId;
            }
            else
            {
                category.ParentId = null;
                Console.WriteLine("Parent Category not found");
            }
            _categoryRepository.Add(category);
            _categoryRepository.SaveChanges();
        }
        public bool DeleteCategory(int id)
        {
            if (!_categoryRepository.GetQuery().Any(x => x.Id == id))
            {
                Console.WriteLine("Category not found");
                return false;
            }
            IQueryable<Entities.CategoryEntity> query = _categoryRepository.GetQuery().Where(x=>x.ParentId == id);
            foreach (var item in query)
            {
                item.ParentId = null;
            }
            _categoryRepository.Delete(_categoryRepository.GetQuery().First(x => x.Id == id));
            _categoryRepository.SaveChanges();
            return true;

        }
        public List<Entities.CategoryEntity> GetAllCategories()
        {
            return _categoryRepository.GetAll().ToList();
        }
        public Entities.CategoryEntity? GetCategoryById(int id)
        {
           return _categoryRepository.GetQuery().FirstOrDefault(x => x.Id == id);
        }
        public bool UpdateCategory(int id, string name)
        {
            var category = _categoryRepository.GetQuery().FirstOrDefault(x => x.Id == id);
            if (category is null)
            {
                Console.WriteLine("Category not found");
                return false;
            }
            else
            {
                category.Name = name;
                _categoryRepository.Update(category);
                _categoryRepository.SaveChanges();
                return true;
            }
        }
    }
}
