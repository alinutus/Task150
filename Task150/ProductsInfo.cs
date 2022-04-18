using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task150
{
    public class ProductsInfo
    {
        public string Description { get; set; }

        public decimal Total { get; set; }

        public ProductsInfo(string description, decimal total)
        {
            Description = description;
            Total = total;
        }
    }
}
