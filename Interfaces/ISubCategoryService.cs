using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OLX.Entities;

namespace OLX.Interfaces
{
    public interface ISubCategoryService
    {
        void CreateSubCategory(string name, string slug);
        List<Subcategory> GetAllSubCategories();
        Subcategory? GetSubCategoryById(int id);
        bool UpdateSubCategory(int id, string name);
        bool DeleteSubCategory(int id);
    }
}
