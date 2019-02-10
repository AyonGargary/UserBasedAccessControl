using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UserBasedAccessControl.Common;
using UserBasedAccessControl.Logging;
using UserBasedAccessControl.Utils;

namespace UserBasedAccessControl.Handlers
{
    class ProductOperation
    {
        ResourceHandler resourceHandler = new ResourceHandler();

        private List<Product> GetProducts()
        {
            List<Product> _products = new List<Product>();
            _products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(Constants.ProductJSONPath));
            return _products;
        }
        #region Product Operations

        /// <summary>
        /// Adds up new Product
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product product)
        {
            if(resourceHandler.GetUser(Login.CurrentUser).Roles != resourceHandler.getRoles(Role.Viewer.ToString()))
            {
                Logger.LogError("Current User has Viewer Access To Product Only");
                return;
            }
            List<Product> products = new List<Product>();
            products = GetProducts();
            products.Add(product);

            string json = JsonConvert.SerializeObject(products);

            if (File.Exists(Constants.ProductJSONPath))
                File.WriteAllText(Constants.ProductJSONPath, json);
        }

        /// <summary>
        /// Deletes Product according to ProductId
        /// </summary>
        /// <param name="productId"></param>
        public void DeleteProduct(int productId)
        {
            if (resourceHandler.GetUser(Login.CurrentUser).Roles != resourceHandler.getRoles(Role.Admin.ToString()))
            {
                Logger.LogError("Current User does not have Admin Access to Delete Product");
                return;
            }
            List<Product> product = GetProducts();
            foreach (Product item in product)
            {
                if (item.Id == productId)
                {
                    product.Remove(item);
                    break;
                }
            }

            string json = JsonConvert.SerializeObject(product);

            if (File.Exists(Constants.ProductJSONPath))
                File.WriteAllText(Constants.ProductJSONPath, json);
        }

        /// <summary>
        /// Updates product w.r.t. Input Product Object
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(Product product)
        {
            if (resourceHandler.GetUser(Login.CurrentUser).Roles != resourceHandler.getRoles(Role.Viewer.ToString()))
            {
                Logger.LogError("Current User has Viewer Access To Product Only");
                return;
            }
            List<Product> products = GetProducts();
            foreach (var item in products)
            {
                if (item.Id == product.Id)
                {
                    item.Id = product.Id;
                    item.ProductName = product.ProductName;
                    item.Quantity = product.Quantity;
                    item.Supplier = product.Supplier;
                    item.Cost = product.Cost;
                    break;
                }
            }

            string json = JsonConvert.SerializeObject(products);

            if (File.Exists(Constants.ProductJSONPath))
                File.WriteAllText(Constants.ProductJSONPath, json);
        }

        /// <summary>
        /// Prints all Product
        /// </summary>
        public void PrintProducts()
        {
            Console.WriteLine(GetProducts());
        }
        #endregion
    }
}
