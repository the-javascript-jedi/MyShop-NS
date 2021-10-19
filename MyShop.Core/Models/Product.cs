using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Product:BaseEntity
    {
        //since we are inheriting from BaseEntity, - The ID is created in BaseEntity class - we can comment the ID 
        //public string Id { get; set; }
        //Add a max string length of 20 to the model
        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0,1000)]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        
        //we create a constructor so that whenever we create a Product model we automatically generate an id
        /*public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }*/
    }                                 
}
