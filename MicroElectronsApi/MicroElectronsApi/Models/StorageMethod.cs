using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class StorageMethod
    {
        public StorageMethod()
        {
            Warehouses = new HashSet<Warehouse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Warehouse> Warehouses { get; set; }
    }
}
