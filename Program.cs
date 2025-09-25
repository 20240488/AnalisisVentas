
using AnalisisVentas.Models;
using AnalisisVentas.Services;
using VentasETL.Data;

class Program
{
    static void Main()
    {
var customers = CsvLoader.LoadCsv<Customer>("Archivos/customers.csv");
var products = CsvLoader.LoadCsv<Product>("Archivos/products.csv");
var orders = CsvLoader.LoadCsv<Order>("Archivos/orders.csv");
var orderDetails = CsvLoader.LoadCsv<OrderDetail>("Archivos/order_details.csv");

orderDetails = DataProcessor.CalculateTotal(orderDetails, products);

var uniqueCustomers = DataProcessor.RemoveDuplicateCustomersByEmail(customers);
var uniqueProducts = DataProcessor.RemoveDuplicateProductsById(products);
var uniqueOrders = DataProcessor.RemoveDuplicateOrdersById(orders);
var uniqueOrderDetails = DataProcessor.RemoveDuplicateOrderDetails(orderDetails);

var validCustomerIDs = uniqueCustomers.Select(c => c.CustomerID).ToHashSet();
var validOrdersFiltered = uniqueOrders.Where(o => validCustomerIDs.Contains(o.CustomerID)).ToList();

var validOrderIDs = validOrdersFiltered.Select(o => o.OrderID).ToHashSet();
var validOrderDetailsFiltered = uniqueOrderDetails.Where(d => validOrderIDs.Contains(d.OrderID)).ToList();

DbLoader.InsertCustomers(uniqueCustomers);
DbLoader.InsertProducts(uniqueProducts);
DbLoader.InsertOrders(validOrdersFiltered);
DbLoader.InsertOrderDetails(validOrderDetailsFiltered);

Console.WriteLine("ETL completado exitosamente.");

    }
}
