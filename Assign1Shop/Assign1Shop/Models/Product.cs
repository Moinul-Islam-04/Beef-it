using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign1Shop.Models
{
    internal class Product
    {

        public string Name { get; set; }

        public string description {get; set;}

        public int price {get; set;}
        public int id {get; set;}

        public int quantity {get; set;}

        public Product()
        {
            Name = "?";
            description = "?";
            price = 0;
            id = 0;
            quantity = 0;

        }

        public Product(string name, string des, int cost, int ID, int Inv_Amount  )
        {
            Name =name;
            description = des;
            price = cost;
            id = ID;
            quantity = Inv_Amount;
        }

        public static int InputConversion(string input )
        {
            int number = 5;
            bool status = false;

            while (!status)
            {

                if (int.TryParse(input, out int result))
                {
                    number = result;
                    status = true;

                }
                else
                {
                    Console.WriteLine("non int was entered please enter again:");
                   input= Console.ReadLine()?? "10";
                    status = false;
                }

            }
            return number;
        }


        public Product DeepCopy()
        {
            return new Product(this.Name, this.description, this.price, this.id, this.quantity);
        }

    }
}
