using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace UserBasedAccessControl.Common
{
    [DataContract]
    class Product
    {
        [DataMember(Name = "_id")]
        public int Id { get; set; }

        [DataMember(Name = "product_name")]
        public string ProductName { get; set; }

        [DataMember(Name = "supplier")]
        public string Supplier { get; set; }

        [DataMember(Name = "quantity")]
        public decimal Quantity { get; set; }

        [DataMember(Name = "unit_cost")]
        public decimal Cost { get; set; }

        public Product(int id, string productName, string supplier, decimal quantity, decimal cost)
        {
            Id = id;
            ProductName = productName;
            Supplier = supplier;
            Quantity = quantity;
            Cost = cost;
        }
    }
}
