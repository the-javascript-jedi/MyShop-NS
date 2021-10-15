using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        //constructor for static initialization
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }
        //we will store our current list of products into the cache.
        public void Commit()
        {
            cache["products"] = products;
        }
        //CRUD functionality
        //Insert
        public void Insert(Product p)
        {
            products.Add(p);
        }
        //Update
        public void Update(Product product)
        {
            //look in db to find if product exists
            Product productToUpdate = products.Find(p => p.Id == product.Id);
            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product no found");
            }
        }
        //Find a single product
        public Product Find(string Id)
        {
            //look in db to find if product exists
            Product product = products.Find(p => p.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        //return a list of products that can be queried
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
        //delete a product
        public void Delete(string Id)
        {
            //look in db to find if product exists
            Product productToDelete = products.Find(p => p.Id == Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found to delete");
            }
        }
    }
}
