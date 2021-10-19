using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory:BaseEntity
    {
        //since we are inheriting from BaseEntity, - The ID is created in BaseEntity class - we can comment the ID 
        //public string Id { get; set; }
        public string Category { get; set; }
        //Constructor to generate Id whenever a new model is created
       /* public ProductCategory()
        {
            this.Id = Guid.NewGuid().ToString();
        }
       */
    }
}
