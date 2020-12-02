using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealerOnSaleTaxes
{
    // enum Representation the categories of product
    public enum EnumCategory
    {
        Basic,
        Book,
        Food,
        Health,
        Import
    }

    // Product class
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int Category { get; set; }
    }

    // Product sold class used to create a list of product from the basket
    public class ProductSold
    {
        public int ProductId { get; set; }
    }
}
