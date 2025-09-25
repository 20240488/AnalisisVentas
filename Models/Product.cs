using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalisisVentas.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}