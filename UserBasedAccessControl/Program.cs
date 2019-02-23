using System;
using UserBasedAccessControl.Common;
using UserBasedAccessControl.Handlers;

namespace UserBasedAccessControl
{
    class Program
    {
        /*
        *Role Based Access Control:
        *
        *Implement a role based auth system. System should be able to assign a role to user and remove a user from the role.
        *
        *Entities are USER, ACTION TYPE, RESOURCE, ROLE
        *
        *ACTION TYPE defines the access level(Ex: READ, WRITE, DELETE)
        *
        *Access to resources for users are controlled strictly by the role.One user can have multiple roles. Given a user, action type and resource system should be able to tell whether user has access or not.
        */
        static void Main(string[] args)
        {

            ProductOperation productOperation = new ProductOperation();
            
            ResourceHandler resourceHandler = new ResourceHandler();

            Console.WriteLine("Please Login\nProvide Username and Password");
            string currentUserName = Console.ReadLine();
            string password = Console.ReadLine();

            Login login = new Login(currentUserName, password);

            if (Login.CurrentUser == null)
            {
                Console.WriteLine("Incorrect Password!! Please restart Appication with correct login");
                Console.ReadKey();
                return;
            }
                

            string choice = "";
            Console.WriteLine("Data Operations\n1. Add Product\t2. Modify Product\t3. Delete product\t4.Print Products");
            while (choice != "exit")
            {
                Console.WriteLine("Make a Choice");
                int c = Convert.ToInt32(Console.ReadLine());
                switch(c)
                {

                    #region Product Cases
                    case 1:
                        Console.WriteLine("Provide Id");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Provide Product Name");
                        string pName = Console.ReadLine();
                        Console.WriteLine("Provide Supplier");
                        string supplier = Console.ReadLine();
                        Console.WriteLine("Provide Quantity");
                        decimal quantity = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Provide Unit Cost");
                        decimal uCost = Convert.ToDecimal(Console.ReadLine());

                        Product product = new Product(id, pName, supplier, quantity, uCost);
                        productOperation.AddProduct(product);
                        break;

                    case 2:
                        Console.WriteLine("Provide Id");
                        id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Provide Product Name");
                        pName = Console.ReadLine();
                        Console.WriteLine("Provide Supplier");
                        supplier = Console.ReadLine();
                        Console.WriteLine("Provide Quantity");
                        quantity = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Provide Unit Cost");
                        uCost = Convert.ToDecimal(Console.ReadLine());

                        product = new Product(id, pName, supplier, quantity, uCost);
                        productOperation.UpdateProduct(product);
                        break;

                    case 3:
                        Console.WriteLine("Provide Id");
                        id = Convert.ToInt32(Console.ReadLine());
                        productOperation.DeleteProduct(id);
                        break;

                    case 4:
                        productOperation.PrintProducts();
                        break;

                    #endregion
                    default:
                        choice = "exit";
                        break;
                }
            }
        }
    }
}
