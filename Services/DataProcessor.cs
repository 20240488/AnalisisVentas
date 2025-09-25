using System;
using System.Collections.Generic;
using System.Linq;
using AnalisisVentas.Models;

namespace AnalisisVentas.Services
{
    public static class DataProcessor
    {
        public static List<OrderDetail> CalculateTotal(List<OrderDetail> details, List<Product> products)
        {
            foreach (var detail in details)
            {
                var product = products.FirstOrDefault(p => p.ProductID == detail.ProductID);
                if (product != null)
                {
                    detail.TotalPrice = detail.Quantity * product.Price;
                }
            }
            return details;
        }

        public static List<T> RemoveDuplicatesByKey<T, TKey>(List<T> list, Func<T, TKey> keySelector)
        {
            return list
                .GroupBy(keySelector)
                .Select(g => g.First())
                .ToList();
        }

        public static List<Customer> RemoveDuplicateCustomersByEmail(List<Customer> customers)
        {
            return RemoveDuplicatesByKey(customers, c => c.Email);
        }

        public static List<Product> RemoveDuplicateProductsById(List<Product> products)
        {
            return RemoveDuplicatesByKey(products, p => p.ProductID);
        }

        public static List<Order> RemoveDuplicateOrdersById(List<Order> orders)
        {
            return RemoveDuplicatesByKey(orders, o => o.OrderID);
        }

        public static List<OrderDetail> RemoveDuplicateOrderDetails(List<OrderDetail> details)
        {
            return RemoveDuplicatesByKey(details, d => new { d.OrderID, d.ProductID });
        }
    }
}
