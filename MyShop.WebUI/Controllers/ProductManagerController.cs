using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        /*
        // ProductRepository - contains our cache memory
        ProductRepository context;
        //ProductCategoryRepository - contains our data from cache
        ProductCategoryRepository productCategories;
        */
        
        /// <summary>
        //using Generic Classes
        /// </summary>
        //InMemoryRepository<Product> context;
        //InMemoryRepository<ProductCategory> productCategories;
        
        /// <summary>
        //using Interfaces
        //even though the declaration has changed here, the rest of the code still works fine because we are then instantiating our concrete implementation of that repository down here.
        /// </summary>
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        //constructor to initialize product repository and productCategories
        //Instantiating the Classes in the constructor
        //public ProductManagerController()
        //{
        //    context = new InMemoryRepository<Product>();
        //    productCategories = new InMemoryRepository<ProductCategory>();            
        //}
        //Instantiating the Interfaces in the constructor
        //we need to inject the interface methods to the constructor, previously we were not using interfaces - we were instantiating an object from the class
        //everytime we create an instance of our ProductManagerController, it must be injected with a class that implements <Product> and a class that implements <ProductCategory>
        public ProductManagerController(IRepository<Product> productContext,IRepository<ProductCategory> productCategoryContext)
        {
            //we refer the interfaces instead of creating instances of the class
            context = productContext;
            productCategories = productCategoryContext;            
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            //return list of products - which we get through the collection
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        //Create Product
        //display the page
        public ActionResult Create()
        {
            //return product with a list of categories to the view
            //create a refernece to the ProductManagerViewModel
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            //Product product = new Product();
            viewModel.Product = new Product();
            //productCategories.Collection(); - we get from db
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

        //Create Product Page which will get the Product POST details
        [HttpPost]
        public ActionResult Create(Product product)
        {
            //Check if model validation is valid or just return the error messages
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                //Insert product to collection
                context.Insert(product);
                //save changes using commit method
                context.Commit();
                //redirect to Index page
                return RedirectToAction("Index");
            }

        }
        //Edit Product - Display the Edit Page
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                //return the view in a view model
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
            }
        }
        //Edit Product - Receives the Edit details in a POST request
        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            //Find the product if it exists
            Product productToEdit = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                //check for model validation and return validation errors
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                //if validation is valid apply the fields to be edited to cache 
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                //commit the edited changes
                context.Commit();
                //redirect to index
                return RedirectToAction("Index");
            }
        }
        //Delete Product
        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }
        //Confirm Delete-DELETE request
        [HttpPost]
        //Alternative action name - Delete
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }


    }
}