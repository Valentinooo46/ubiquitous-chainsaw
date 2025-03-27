using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OLX.Interfaces;
using OLX.Entities;

namespace OLX.Implements
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly IRepository<Subcategory> _subCategoryRepository;
        public SubCategoryService(IRepository<Subcategory> subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }
        public void CreateSubCategory(string name, string slug)
        {
            var subCategory = new Subcategory
            {
                Name = name,
                Slug = slug
            };
            _subCategoryRepository.Add(subCategory);
            _subCategoryRepository.SaveChanges();
        }
        public List<Subcategory> GetAllSubCategories()
        {
            return _subCategoryRepository.GetAll().ToList();
        }
        public Subcategory? GetSubCategoryById(int id)
        {
            return _subCategoryRepository.GetById(id);
        }
        public bool UpdateSubCategory(int id, string name)
        {
            var subCategory = _subCategoryRepository.GetById(id);
            if (subCategory == null)
            {
                return false;
            }
            subCategory.Name = name;
            _subCategoryRepository.Update(subCategory);
            _subCategoryRepository.SaveChanges();
            return true;
        }
        public bool DeleteSubCategory(int id)
        {
            var subCategory = _subCategoryRepository.GetById(id);
            if (subCategory == null)
            {
                return false;
            }
            _subCategoryRepository.Delete(subCategory);
            _subCategoryRepository.SaveChanges();
            return true;
        }
    }
}