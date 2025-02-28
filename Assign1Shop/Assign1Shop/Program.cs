using Assign1Shop.Models;
using System;

namespace Assign1Shop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string Selection = "";

            List<Product> inventory = new List<Product>();

            while (Selection != "x")
            {

                Console.WriteLine("Enter a choice (enter 'x' to exit, 's' to shop or 'i' for inventory): ");
                Selection = Console.ReadLine() ?? "x";

                if (Selection == "s")
                {
                    string Shop_Selection = "";
                    List<Product> cart = new List<Product>();

                    while (Shop_Selection != "x")
                    {

                        Console.WriteLine("Welcome to the shoping menu:S to shop, r to read cart, c to checkout or x to quit. ");
                        Shop_Selection = Console.ReadLine() ?? "x";


                        if (Shop_Selection == "s")
                        {
                            Console.WriteLine("Here is the inventory list:");
                            for (int i = 0; i < inventory.Count; i++)
                            {
                                Console.WriteLine($"ITEM: {inventory[i].Name}. DESCRIPTION: {inventory[i].description}. PRICE: {inventory[i].price}. QUANTITY: {inventory[i].quantity} ID: {inventory[i].id}");
                            }

                            Console.WriteLine("Please enter the ID of the item to add to the cart:");
                            string input = Console.ReadLine() ?? "0";
                            int Sel_Item = Product.InputConversion(input);

                            bool itemFound = false;

                            for (int i = 0; i < inventory.Count; i++)
                            {
                                if (inventory[i].id == Sel_Item)
                                {
                                    itemFound = true;
                                    Console.WriteLine("Please enter the desired amount of the item:");
                                    string Q_Input = Console.ReadLine() ?? "0";
                                    int Amount = Product.InputConversion(Q_Input);

                                    if (Amount > 0 && Amount <= inventory[i].quantity)
                                    {
                                        bool cartItemFound = false;

                                        for (int x = 0; x < cart.Count; x++)
                                        {
                                            if (cart[x].id == Sel_Item)
                                            {
                                                cart[x].quantity = Amount;  
                                                cartItemFound = true;
                                                break;
                                            }
                                        }

                                        if (!cartItemFound)
                                        {
                                            Product newProduct = inventory[i].DeepCopy();
                                            newProduct.quantity = Amount;
                                            cart.Add(newProduct);
                                        }

                                        Console.WriteLine("Current cart items:");
                                        for (int x = 0; x < cart.Count; x++)
                                        {
                                            Console.WriteLine($"Cart ITEM: {cart[x].Name} AMOUNT: {cart[x].quantity} ID: {cart[x].id}");
                                        }

                                        Console.WriteLine("Item added to cart");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid number entered");
                                    }

                                    break;
                                }
                            }

                            if (!itemFound)
                            {
                                Console.WriteLine("No item with the given ID found");
                            }
                        }

                        if (Shop_Selection == "r")
                        {
                            for (int i = 0; i < cart.Count; i++)
                            {
                                Console.WriteLine($"Cart ITEM: {cart[i].Name} AMOUNT: {cart[i].quantity} ID: {cart[i].id}");
                            }

                            Console.WriteLine("Would you like to remove an item? If so, enter its ID in the cart, else enter 'x':");
                            string choice = Console.ReadLine();

                            if (choice != "x")
                            {
                                int del = Product.InputConversion(choice);

                                bool itemRemoved = false;
                                for (int x = 0; x < cart.Count; x++)
                                {
                                    if (cart[x].id == del)
                                    {
                                        cart.RemoveAt(x);
                                        itemRemoved = true;
                                        Console.WriteLine($"Item with ID {del} removed from the cart.");
                                        break; 
                                    }
                                }

                                if (!itemRemoved)
                                {
                                    Console.WriteLine($"No item with ID {del} found in the cart.");
                                }
                            }
                        }

                        if (Shop_Selection == "c")
                        {
                            double total = 0;
                            for (int i = 0; i < cart.Count; i++)
                            {
                                total += cart[i].price * cart[i].quantity;
                            }

                            total += total * 0.07;

                            Console.WriteLine($"Your total is: ${total}");
                            Console.WriteLine("Would you like to finish checking out? (y/n)");

                            string select = Console.ReadLine() ?? "n";

                            if (select == "y")
                            {
                                for (int x = 0; x < cart.Count; x++)
                                {
                                    for (int i = 0; i < inventory.Count; i++)
                                    {
                                        if (cart[x].id == inventory[i].id)
                                        {
                                            inventory[i].quantity -= cart[x].quantity;
                                            break; 
                                        }
                                    }
                                }

                                for (int x = 0; x < cart.Count; x++)
                                {
                                    Console.WriteLine($"[{x}] {cart[x].Name}: quantity {cart[x].quantity}: ${cart[x].price * cart[x].quantity}");
                                }

                                Console.WriteLine($"${total}");

                                
                                cart.Clear();
                                Console.WriteLine("Your cart has been cleared after checkout.");
                            }
                        }


                    }
                }

                if (Selection == "i")
                {
                    string inv_Selection = "";

                    while (inv_Selection != "x")
                    {


                        Console.WriteLine("Enter a choice (enter 'c' to create, 'r' to read 'u' to update or 'd' to delete): x to quit ");
                        inv_Selection = Console.ReadLine() ?? "x";

                        if (inv_Selection == "c")
                        {
                            Console.WriteLine("Please enter the ID of the new item: ");
                            var inv_ID = Console.ReadLine();

                            int Price = 9; 
                            int Amount = 1;

                            if (int.TryParse(inv_ID, out int index))
                            {
                                bool unique_Id = true;

                                
                                for (int i = 0; i < inventory.Count; i++)
                                {
                                    if (inventory[i].id == index) 
                                    {
                                        Console.WriteLine("You have entered an ID that is already present in the inventory.");
                                        unique_Id = false;
                                        break;
                                    }
                                }

                                if (unique_Id)
                                {
                                    Console.WriteLine("Enter product name: ");
                                    var inventory_ID = Console.ReadLine();

                                    Console.WriteLine("Enter product description: ");
                                    var inventory_product = Console.ReadLine();

                                    Console.WriteLine("Enter product price: ");
                                    var inventory_Price = Console.ReadLine();

                                    Price = Product.InputConversion(inventory_Price);

                                    Console.WriteLine("please enter ammount of item");
                                    var inventory_Amount = Console.ReadLine();

                                    Amount = Product.InputConversion(inventory_Amount);

                                    
                                    Product newProduct = new Product(inventory_ID, inventory_product, Price, index, Amount);
                                    inventory.Add(newProduct);
                                    Console.WriteLine("New item added.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID. Please enter a valid integer.");
                            }
                        }

                        if (inv_Selection == "r")
                        {
                            Console.WriteLine("Reading all items in the inventory:");

                            for (int i = 0; i < inventory.Count; i++)
                            {
                                Console.WriteLine("Name: " + inventory[i].Name);
                                Console.WriteLine("Description: " + inventory[i].description);
                                Console.WriteLine("Price: " + inventory[i].price);
                                Console.WriteLine("Id: " + inventory[i].id);
                                Console.WriteLine("Quantity: " + inventory[i].quantity);
                                Console.WriteLine(); 
                            }
                        }

                        if (inv_Selection == "u")
                        {
                            Console.WriteLine("please enter item ID to update ");
                            var Id_Space = Console.ReadLine();


                            if (int.TryParse(Id_Space, out int index))
                            {

                                bool itemFound = false;
                                for (int i = 0; i < inventory.Count; i++)
                                {
                                    if (inventory[i].id == index)
                                    {
                                        Console.WriteLine("Item found ");
                                        Console.WriteLine("please enter new name");
                                        inventory[i].Name = Console.ReadLine() ?? "Default";

                                        Console.WriteLine("Please enter new description");
                                        inventory[i].description = Console.ReadLine() ?? "Default";

                                        int Price = 0;
                                        int Quantity = 0;

                                        Console.WriteLine("Please enter new price");
                                        string new_price = Console.ReadLine() ?? "10";
                                        Price = Product.InputConversion(new_price);

                                        Console.WriteLine("Please enter new quantity");
                                        string new_Quantity = Console.ReadLine() ?? "10";
                                        Quantity = Product.InputConversion(new_Quantity);

                                        inventory[i].price = Price;
                                        inventory[i].quantity = Quantity;

                                        itemFound = true;
                                        break;

                                    }
                                    
                                }

                                if (!itemFound)
                                {
                                    Console.WriteLine("ID not found in the inventory.");
                                }

                            }

                        }


                        if (inv_Selection == "d")
                        {
                            Console.WriteLine("please enter item ID to delete ");
                            var Id_delete = Console.ReadLine();

                            if (int.TryParse(Id_delete, out int index))
                            {
                                bool itemDeleted = false;

                                for (int i = 0; i < inventory.Count; i++)
                                {
                                    if (inventory[i].id == index)
                                    {
                                        Console.WriteLine("item has been deleted");
                                        inventory.RemoveAt(i);
                                        itemDeleted = true;
                                        break;
                                    }

                                }
                                if (!itemDeleted)
                                {
                                    Console.WriteLine("ID not found in the inventory.");
                                }

                            }
                        }
                    }
                }
            }
        }
    }


}
