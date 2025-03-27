using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OLX.Entities;

namespace OLX.Interfaces
{
    interface IProductService
    {
        void CreateProduct(string name, decimal price, int categoryId);
        List<ProductEntity> GetAllProducts();
        ProductEntity? GetProductById(int id);
        bool UpdateProduct(int id, string name);
        bool DeleteProduct(int id);
    }
}
