using Angular4WebApi.DBModel;
using Angular4WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Angular4WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private readonly InventoryEntities _context;

        public ProductController()
        {
            _context = new InventoryEntities();
        }
        // GET api/<controller>
        [Route("api/Product/GetProducts")]
        public IEnumerable<ProductJSON> GetProducts()
        {
            IQueryable<ProductJSON> products = _context.Product.Select(
                    p => new ProductJSON
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Category = p.Category,
                        Price = p.Price
                    });
            return products.ToList();
        }
        // POST api/<controller>
        public Product Post([FromBody]Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product");
            }

            Product newProduct = new Product();

            try
            {
                newProduct.Name = product.Name;
                newProduct.Category = product.Category;
                newProduct.Price = product.Price;
                _context.Product.Add(newProduct);
                int rowsAffected = _context.SaveChanges();

                return rowsAffected > 0 ? product : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        // PUT api/<controller>/5
        public bool Put(int id, [FromBody]Product p)
        {
            p.Id = id;

            if (p == null)
            {
                throw new ArgumentNullException("p");
            }

            using (var ctx = new InventoryEntities())
            {
                var product = _context.Product.Single(a => a.Id == p.Id);

                if (product != null)
                {
                    product.Name = p.Name;
                    product.Category = p.Category;
                    product.Price = p.Price;

                    int rowsAffected = _context.SaveChanges();

                    return rowsAffected > 0 ? true : false;
                }
                else
                {
                    return false;
                }
            }
        }
        // DELETE api/<controller>/5
        public bool Delete(int id)
        {
            using (var ctx = new InventoryEntities())
            {
                Product products = ctx.Product.Find(id);
                ctx.Product.Remove(products);

                int rowsAffected = ctx.SaveChanges();

                return rowsAffected > 0 ? true : false;
            }
        }
    }
}