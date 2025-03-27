using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OLX.Entities;


namespace OLX.Interfaces
{
    interface INewCategoryService
    {
        void CreateCategory(string name, string slug,ICollection<Subcategory> subcategories);
        List<Category> GetAllCategories();
        Category? GetCategoryById(int id);
        bool UpdateCategory(int id, string name);
        bool DeleteCategory(int id);
    }
}
