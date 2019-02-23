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
            Object _lockObject = null;
            lock (_lockObject)
            {
                List<Product> products = new List<Product>();
                products = GetProducts();
                products.Add(product);

                string json = JsonConvert.SerializeObject(products);

                if (File.Exists(Constants.ProductJSONPath))
                    File.WriteAllText(Constants.ProductJSONPath, json);
            }
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
            Object _lockObject = null;
            lock (_lockObject)
            {
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
        }

        public void BuyProduct(string product, int amount)
        {
            Product p = null;
            foreach(Product _product in GetProducts())
            {
                if(_product.ProductName.Equals(product,StringComparison.InvariantCultureIgnoreCase))
                {
                    p = _product;
                }
            }
            if (p == null)
            {
                Logger.LogError("No Such Product found");
                return;
            }
            BuyProduct(p, amount);
        }

        /// <summary>
        /// Buy Inventory function, checks if product exists
        /// If quantity exists, product can be bought, and quantity reduces by amount 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="amount"></param>
        private void BuyProduct(Product product, int amount)
        {
            if(product.Quantity < amount)
            {
                Logger.LogError("Product can't be bought due to insufficient quantity");
                return;
            }
            else
            {
                product.Quantity = product.Quantity - amount;
                UpdateProduct(product);
            }
        }

        public void ReturnProduct(string product, int amount)
        {
            Product p = null;
            foreach (Product _product in GetProducts())
            {
                if (_product.ProductName.Equals(product, StringComparison.InvariantCultureIgnoreCase))
                {
                    p = _product;
                }
            }
            ReturnProduct(p, amount);
        }

        /// <summary>
        /// Buy Inventory function, checks if product exists
        /// If quantity exists, product can be bought, and quantity reduces by amount 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="amount"></param>
        private void ReturnProduct(Product product, int amount)
        {
            if (product != null)
            {
                product.Quantity = product.Quantity - amount;
                UpdateProduct(product);
            }
            else
            {
                Logger.LogInfo("Product not present!! Adding product");
                AddProduct(product);
            }
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
            Object _lockObject = null;
            lock (_lockObject)
            {
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
        }

        /// <summary>
        /// Prints all Product
        /// Read read operation no locking required
        /// </summary>
        public void PrintProducts()
        {
            Console.WriteLine(GetProducts());
        }
        #endregion
    }
}
