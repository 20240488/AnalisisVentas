using System;
using System.Collections.Generic;
using System.Linq;
using AnalisisVentas.Models;

namespace AnalisisVentas.Services
{
    public static class DataProcessor
    {
        // Calcula el Total de OrderDetails
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

        // Elimina duplicados de una lista por un campo clave específico
        public static List<T> RemoveDuplicatesByKey<T, TKey>(List<T> list, Func<T, TKey> keySelector)
        {
            return list
                .GroupBy(keySelector)
                .Select(g => g.First())
                .ToList();
        }

        // Método específico para Customers por Email
        public static List<Customer> RemoveDuplicateCustomersByEmail(List<Customer> customers)
        {
            return RemoveDuplicatesByKey(customers, c => c.Email);
        }

        // Método específico para Products por ProductID
        public static List<Product> RemoveDuplicateProductsById(List<Product> products)
        {
            return RemoveDuplicatesByKey(products, p => p.ProductID);
        }

        // Método específico para Orders por OrderID
        public static List<Order> RemoveDuplicateOrdersById(List<Order> orders)
        {
            return RemoveDuplicatesByKey(orders, o => o.OrderID);
        }

        // Método específico para OrderDetails por OrderID + ProductID
        public static List<OrderDetail> RemoveDuplicateOrderDetails(List<OrderDetail> details)
        {
            return RemoveDuplicatesByKey(details, d => new { d.OrderID, d.ProductID });
        }
    }
}
