using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    //to define the class as a generic class, we define a placeholder right after our class declaration
    //<T>- we will reference during the type rest of our code
    //whenever we pass an object it must be inherit from type BaseEntity - so BaseEntity has an id property so whenever we reference an id from a generic type, our generic class knows the class will have an id property - i=>i.Id==t.Id
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        //create an object cache
        ObjectCache cache = MemoryCache.Default;
        //A list which references the type of placeholder
        List<T> items;
        string className;

        //Constructor
        public InMemoryRepository()
        {
            //gets the name of the passed class
            className = typeof(T).Name;
            //check if any items are in cache
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }

        //commit the changes to memory
        public void Commit()
        {
            cache[className] = items;
        }
        //Insert
        public void Insert(T t)
        {
            items.Add(t);
        }

        //Update
        public void Update(T t)
        {
            T tToUpdate = items.Find(i => i.Id == t.Id);
            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(className + " Not Found");
            }
        }
        //Find
        public T Find(string Id)
        {
            T t = items.Find(i => i.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + " Not Found");
            }
        }
        //Collection
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        //Delete method
        public void Delete(string Id)
        {
            T tToDelete = items.Find(i => i.Id == Id);
            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(className + " Not Found");
            }
        }        
    }
}
