using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealerOnSaleTaxes
{
    public class StoreService
    {
        public static List<Product> productsContainer = new List<Product>(); // Main DataObject
        public static List<ProductSold> productsChecked = new List<ProductSold>(); // Main DataObject collecting items from the basket
        public static double salesTax = 0.0; // Total salesTaxes
        public static double salesTotal = 0.0; // Total Sales

        // Load Sample Data
        public static void LoadProducts()
        {
            productsContainer = new List<Product>() {
                new Product(){ ProductId =1, ProductName ="Book", ProductPrice= 12.49, Category= (int)EnumCategory.Book },
                new Product(){ ProductId =2, ProductName="Music CD", ProductPrice= 18.49, Category= (int)EnumCategory.Basic },
                new Product(){ ProductId =3, ProductName="Chocolate", ProductPrice= 16.49, Category= (int)EnumCategory.Food },
                new Product(){ ProductId =4, ProductName="Chocolate bar", ProductPrice= 0.85, Category= (int)EnumCategory.Food },
                new Product(){ ProductId =5, ProductName="Imported box of chocolates", ProductPrice= 54.65, Category= (int)EnumCategory.Import },
                new Product(){ ProductId =6, ProductName="Imported bottle of perfume", ProductPrice= 32.19, Category= (int)EnumCategory.Import },
                new Product(){ ProductId =7, ProductName="Bottle of perfume", ProductPrice= 20.89, Category= (int)EnumCategory.Basic },
                new Product(){ ProductId =8, ProductName="Packet of headache pills", ProductPrice= 9.75, Category= (int)EnumCategory.Health },
                new Product(){ ProductId =9, ProductName="Imported box of chocolates", ProductPrice= 11.85, Category= (int)EnumCategory.Import }
            };
        }

        //  Display Header for the menu
        public static void DisplayHeader()
        {
            Console.Clear();
            Console.WriteLine("------ DealerOn Store Dashboard ------");
            Console.WriteLine("");
        }

        // Display Main menu
        public static void DisplayMainMenu()
        {
            DisplayHeader();
            Console.WriteLine("@ Press 1 to Check out products ");
            Console.WriteLine("@ Press 2 to Display Product Menu ");
            Console.WriteLine("@ Press 3 to exit store");
            Console.WriteLine("");
            Console.Write("Enter your choice here: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CheckOutProducts();
                    break;
                case "2":
                    DisplayProductMenu();
                    break;
                case "3":
                    Environment.Exit(1);
                    break;
                Default:
                    break;
            }
        }

        // This function helps build the list of items from the basket
        public static void CheckOutProducts()
        {
            Console.WriteLine("Type 'C' anytime to cancel this operation. "); // User can exit the process by typing C
            Console.WriteLine("");


            var productId = 0;
            var inputText = "";
            Int32.TryParse(inputText, out productId);
            // Check if ProductId input exist in out sample data, otherwise keep asking same input to user
            while (!productsContainer.Where(x => x.ProductId == productId).Any() && inputText.ToLower() != "c")
            {
                Console.WriteLine("Enter Product ID: ");
                inputText = Console.ReadLine();
                Int32.TryParse(inputText, out productId);
            }

            // If user decides to abort then go back to MainMenu
            if (inputText.ToLower() == "c")
            {
                DisplayMainMenu();
            }
            else
            {
                productsChecked.Add(new ProductSold() { ProductId = productId });
                Console.WriteLine(" ");
                foreach (var item in productsChecked)
                {
                    var product = productsContainer.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                    Console.WriteLine("1 " + product.ProductName + " at " + product.ProductPrice);
                }
                Console.WriteLine(" ");
                Console.WriteLine("Do you want to check out more products? Y or N ");
                if (Console.ReadLine().ToLower() == "y") { CheckOutProducts(); } else { ShowReceipt(); };
            }
        }

        // Build and display Receipt
        public static void ShowReceipt()
        {
            salesTotal = 0.0;
            salesTax = 0.0;
            // Grouped products from the basket
            var groupedItems = productsChecked.GroupBy(x => x.ProductId).Select(y => new { ProductId = y.Key, Quantity = y.Count() }).ToList();

            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("Your Receipt");
            Console.WriteLine("");
            foreach (var item in groupedItems)
            {
                var product = productsContainer.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                GetTax(product, item.Quantity);
            }
            Console.WriteLine("Sales Taxes: " + Math.Round(salesTax, 2));
            Console.WriteLine("Total: " + Math.Round(salesTotal, 2));
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            //DisplayMainMenu();
        }


        // Calculate the respective tax for each item
        public static void GetTax(Product item, int count)
        {
            var tax = 0.0;
            var cost = 0.0;

            switch (item.Category)
            {
                case (int)EnumCategory.Basic:
                    tax = ((item.ProductPrice * count) * 10) / 100;
                    tax = Math.Round(tax * 20, 2) / 20;
                    salesTax = salesTax + tax;
                    cost = (item.ProductPrice * count) + tax;
                    salesTotal = salesTotal + cost;
                    Console.WriteLine(item.ProductName + ": " + Math.Round(cost, 2) + " (" + count + " @ " + item.ProductPrice + ")");
                    break;
                case (int)EnumCategory.Import:
                    tax = ((item.ProductPrice * count) * 5) / 100;
                    tax = Math.Round(tax * 20, 2) / 20;
                    salesTax = salesTax + tax;
                    cost = (item.ProductPrice * count) + tax;
                    salesTotal = salesTotal + cost;
                    Console.WriteLine(item.ProductName + ": " + Math.Round(cost, 2) + " (" + count + " @ " + item.ProductPrice + ")");
                    break;
                default:
                    cost = (item.ProductPrice * count);
                    salesTotal = salesTotal + cost;
                    Console.WriteLine(item.ProductName + ": " + Math.Round(cost, 2) + " (" + count + " @ " + item.ProductPrice + ")");
                    break;
            }
        }


        // Display ProductMenu
        public static void DisplayProductMenu()
        {
            DisplayHeader();
            Console.WriteLine("@ Press 1 to Search products ");
            Console.WriteLine("@ Press 2 to Add products ");
            Console.WriteLine("@ Press 3 to Go back to Main Menu ");
            Console.WriteLine("@ Press 4 to exit store");
            Console.WriteLine("");
            Console.Write("Enter your choice here: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    DisplayProducts();
                    break;
                case "2":
                    AddProduct();
                    break;
                case "3":
                    DisplayMainMenu();
                    break;
                case "4":
                    Environment.Exit(1);
                    break;
                Default:
                    break;
            }
        }


        // Display list of prodcut from the sample data
        public static void DisplayProducts()
        {
            DisplayHeader();
            Console.WriteLine("| DealerOn Products Catalogue |");

            Console.WriteLine("");
            Console.WriteLine(" ------------------------------------------------------------------------------------------");
            Console.WriteLine("| Product ID       | Product Name              | Product Price         | Product Category  |");
            Console.WriteLine(" ------------------------------------------------------------------------------------------");

            foreach (var product in productsContainer.OrderBy(x => x.ProductName))
            {
                #region DECORATION
                // Decoration purposes only
                var decorId = new StringBuilder();
                var decorName = new StringBuilder();
                var decorPrice = new StringBuilder();
                var decorCat = new StringBuilder();
                for (var i = 0; i < (17 - product.ProductId.ToString().Length); i++)
                {
                    decorId.Append(" ");
                }
                for (var i = 0; i < (26 - product.ProductName.Length); i++)
                {
                    decorName.Append(" ");
                }
                for (var i = 0; i < (22 - product.ProductPrice.ToString().Length); i++)
                {
                    decorPrice.Append(" ");
                }
                for (var i = 0; i < (18 - Enum.GetName(typeof(EnumCategory), product.Category).Length); i++)
                {
                    decorCat.Append(" ");
                }
               # endregion
                Console.WriteLine("| " + product.ProductId + decorId.ToString() + "| " + product.ProductName + decorName.ToString() + "| " + product.ProductPrice + decorPrice.ToString() + "| " + Enum.GetName(typeof(EnumCategory), product.Category) + decorCat.ToString() + "|");
            }
            Console.WriteLine(" ------------------------------------------------------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("Return to Product Menu? Y or N  ");
            if (Console.ReadLine().ToLower() == "y") DisplayProductMenu();
        }


        // ADD NEW   Product to the sample data
        public static void AddProduct()
        {
            DisplayHeader();
            Console.WriteLine("Type 'C' anytime to cancel this operation. ");
            Console.WriteLine("Below is a list of product category - Please choose corresponding number when requested. ");

            foreach (var item in (EnumCategory[])Enum.GetValues(typeof(EnumCategory)))
            {
                Console.WriteLine((int)item + " - " + Enum.GetName(typeof(EnumCategory), (int)item));
            }
            Console.WriteLine("");


            var productName = "";
            while (string.IsNullOrEmpty(productName) && productName.ToLower() != "c")
            {
                Console.WriteLine("Enter Product Name: ");
                productName = Console.ReadLine();
            }

            if (productName.ToLower() == "c")
            {
                DisplayProductMenu();
            }
            else
            {
                var productPrice = -1.0;
                var inputText = "-1.0";
                while ((productPrice <= 0 || !Double.TryParse(inputText, out productPrice)) && inputText.ToLower() != "c")
                {
                    Console.WriteLine("Enter Unit Price: ");
                    inputText = Console.ReadLine();
                    Double.TryParse(inputText, out productPrice);
                }

                if (inputText.ToLower() == "c")
                {
                    DisplayProductMenu();
                }
                else
                {
                    var productCategory = 0;
                    inputText = "-1";
                    Int32.TryParse(inputText, out productCategory);

                    while (string.IsNullOrEmpty(Enum.GetName(typeof(EnumCategory), productCategory)) && inputText.ToLower() != "c")
                    {
                        Console.WriteLine("Enter Product Category: ");
                        inputText = Console.ReadLine();
                        Int32.TryParse(inputText, out productCategory);
                    }

                    if (inputText.ToLower() == "c")
                    {
                        DisplayProductMenu();
                    }
                    else
                    {
                        var pId = productsContainer.OrderByDescending(x => x.ProductId).FirstOrDefault().ProductId + 1;
                        productsContainer.Add(new Product() { ProductId = pId, ProductName = productName, ProductPrice = productPrice, Category = productCategory });
                        Console.WriteLine("Your product has been added successfully! ");
                        Console.WriteLine("Do you want to add one more product? Y or N ");
                        if (Console.ReadLine().ToLower() == "y") { AddProduct(); } else { DisplayProductMenu(); };
                    }
                }

            }
        }
        }
}
