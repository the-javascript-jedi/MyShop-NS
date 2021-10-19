﻿using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class ProductManagerViewModel
    {
        public Product Product
        {
            get;
            set;
        }

        //create an enumerable list which we can iterate through
        public IEnumerable<ProductCategory> ProductCategories
        {
            get;
            set;
        }
    }
}
