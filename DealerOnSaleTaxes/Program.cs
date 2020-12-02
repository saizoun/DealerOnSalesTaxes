using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DealerOnSaleTaxes
{
     class Program
    {
        /*The best approach in modern software inmplementation is separation of concern
         * Which I did illustrate with Program.cs, StoreModel and StoreService
         * A lot more functions could have been added or a database approach could have been implemented but I wanted to keep it basic to 
         * display my thoughts process.
         * 
         * Author: Sarkis Aizoun
         * Date: 12-02-2020
         */
       
         static void Main(string[] args)
        {
            // Load the sample Data
            StoreService.LoadProducts();
            StoreService.DisplayMainMenu(); //Display the Main or Parent Menu
        }
    }

   
}
