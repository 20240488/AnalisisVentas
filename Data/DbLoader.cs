using System.Data;
using Microsoft.Data.SqlClient;
using AnalisisVentas.Models;

namespace VentasETL.Data;

public static class DbLoader
{
    private static string connectionString = "Server=localhost,1433; Database=AnalisisVentas; User ID=sa; Password=WendellAGPB@0305_; TrustServerCertificate=True";

    public static void InsertCustomers(List<Customer> customers)
    {
        using var conn = new SqlConnection(connectionString);
        conn.Open();

        foreach (var c in customers)
        {
            using var cmd = new SqlCommand("InsertCustomer", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerID", c.CustomerID);
            cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
            cmd.Parameters.AddWithValue("@LastName", c.LastName);
            cmd.Parameters.AddWithValue("@Email", c.Email);
            cmd.Parameters.AddWithValue("@Phone", c.Phone);
            cmd.Parameters.AddWithValue("@City", c.City);
            cmd.Parameters.AddWithValue("@Country", c.Country);
            cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"{customers.Count} Clientes insertados.");
    }

    public static void InsertProducts(List<Product> products)
    {
        using var conn = new SqlConnection(connectionString);
        conn.Open();

        foreach (var p in products)
        {
            using var cmd = new SqlCommand("InsertProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductID", p.ProductID);
            cmd.Parameters.AddWithValue("@ProductName", p.ProductName);
            cmd.Parameters.AddWithValue("@Category", p.Category);
            cmd.Parameters.AddWithValue("@Price", p.Price);
            cmd.Parameters.AddWithValue("@Stock", p.Stock);
            cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"{products.Count} productos insertados.");
    }

    public static void InsertOrders(List<Order> orders)
    {
        using var conn = new SqlConnection(connectionString);
        conn.Open();

        foreach (var o in orders)
        {
            using var cmd = new SqlCommand("InsertOrder", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderID", o.OrderID);
            cmd.Parameters.AddWithValue("@CustomerID", o.CustomerID);
            cmd.Parameters.AddWithValue("@OrderDate", o.OrderDate);
            cmd.Parameters.AddWithValue("@Status", o.Status);
            cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"{orders.Count} ordenes insertadas.");
    }

    public static void InsertOrderDetails(List<OrderDetail> details)
    {
        using var conn = new SqlConnection(connectionString);
        conn.Open();

        foreach (var d in details)
        {
            using var cmd = new SqlCommand("InsertOrderDetail", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderID", d.OrderID);
            cmd.Parameters.AddWithValue("@ProductID", d.ProductID);
            cmd.Parameters.AddWithValue("@Quantity", d.Quantity);
            cmd.Parameters.AddWithValue("@TotalPrice", d.TotalPrice);
            cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"{details.Count} detalles de ordenes insertadas.");
    }
}
