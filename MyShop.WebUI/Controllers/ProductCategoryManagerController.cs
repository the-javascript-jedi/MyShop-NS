using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        // ProductCategoryRepository - contains our cache memory
        //ProductCategoryRepository context;
        //use the generic class
        InMemoryRepository<ProductCategory> context;

        //constructor to initialize product repository
        public ProductCategoryManagerController()
        {
            //context = new ProductCategoryRepository();
            //use the generic class
            context = new InMemoryRepository<ProductCategory>();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            //return list of products - which we get through the collection
            List<ProductCategory> productCategoriess = context.Collection().ToList();
            return View(productCategoriess);
        }
        //Create Product
        //display the page
        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        //Create Product Page which will get the Product POST details
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            //Check if model validation is valid or just return the error messages
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                //Insert product to collection
                context.Insert(productCategory);
                //save changes using commit method
                context.Commit();
                //redirect to Index page
                return RedirectToAction("Index");
            }

        }
        //Edit Product - Display the Edit Page
        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }
        //Edit Product - Receives the Edit details in a POST request
        [HttpPost]
        public ActionResult Edit(ProductCategory product, string Id)
        {
            //Find the product if it exists
            ProductCategory productCategoryToEdit = context.Find(Id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                //check for model validation and return validation errors
                if (!ModelState.IsValid)
                {
                    return View(productCategoryToEdit);
                }
                //if validation is valid apply the fields to be edited to cache 
                productCategoryToEdit.Category = product.Category;               
                //commit the edited changes
                context.Commit();
                //redirect to index
                return RedirectToAction("Index");
            }
        }
        //Delete Product
        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }
        //Confirm Delete-DELETE request
        [HttpPost]
        //Alternative action name - Delete
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
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