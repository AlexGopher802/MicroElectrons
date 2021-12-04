using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroElectronsApi.Models.Data
{
    public class ProductData
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string StorageMethod { get; set; }
    }
}
