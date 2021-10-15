using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        // ProductRepository - contains our cache memory
        ProductRepository context;
        //constructor to initialize product repository
        public ProductManagerController()
        {
            context = new ProductRepository();
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
            Product product = new Product();
            return View(product);
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
                return View(product);
            }
        }
        //Edit Product - Receives the Edit details in a POST request
        [HttpPost]
        public ActionResult Edit(Product product,string Id)
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