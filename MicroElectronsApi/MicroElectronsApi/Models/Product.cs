using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class Product
    {
        public Product()
        {
            SupplyCompos = new HashSet<SupplyCompo>();
            Warehouses = new HashSet<Warehouse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public virtual ProductCategory Category { get; set; }
        public virtual ICollection<SupplyCompo> SupplyCompos { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; }
    }
}
