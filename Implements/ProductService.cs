using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLX.Implements
{
    public class ProductService : Interfaces.IProductService
    {
        private readonly Interfaces.IRepository<Entities.ProductEntity> _productRepository;
        public ProductService(Interfaces.IRepository<Entities.ProductEntity> productRepository)
        {
            _productRepository = productRepository;
        }
        public void CreateProduct(string name, decimal price, int categoryId)
        {
            var product = new Entities.ProductEntity
            {
                Name = name,
                Price = price,
                CategoryId = categoryId,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now)
            };
            _productRepository.Add(product);
            _productRepository.SaveChanges();
        }
        public List<Entities.ProductEntity> GetAllProducts()
        {
            return _productRepository.GetAll().ToList();
        }
        public Entities.ProductEntity? GetProductById(int id)
        {
            return _productRepository.GetById(id);
        }
        public bool UpdateProduct(int id, string name)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return false;
            }
            product.Name = name;
            _productRepository.Update(product);
            _productRepository.SaveChanges();
            return true;
        }
        public bool DeleteProduct(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return false;
            }
            _productRepository.Delete(product);
            _productRepository.SaveChanges();
            return true;
        }
    }
}
