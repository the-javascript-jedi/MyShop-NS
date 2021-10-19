using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        //constructor for static initialization
        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }
        //we will store our current list of products into the cache.
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }
        //CRUD functionality
        //Insert
        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }
        //Update
        public void Update(ProductCategory productCategory)
        {
            //look in db to find if product exists
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);
            if (productCategory != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }
        //Find a single product
        public ProductCategory Find(string Id)
        {
            //look in db to find if product category exists
            ProductCategory productCategory = productCategories.Find(p => p.Id == Id);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }
        //return a list of products that can be queried
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }
        //delete a product
        public void Delete(string Id)
        {
            //look in db to find if product exists
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);
            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category not found to delete");
            }
        }
    }
}

