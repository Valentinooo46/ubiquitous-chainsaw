using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OLX.Entities;
namespace OLX.Interfaces
{
    interface ICategoryService
    {
        void CreateCategory(string name,string Description,int ParentId = 0);
        List<CategoryEntity> GetAllCategories();
        CategoryEntity? GetCategoryById(int id);
        bool UpdateCategory(int id, string name);
        bool DeleteCategory(int id);
    }
}
