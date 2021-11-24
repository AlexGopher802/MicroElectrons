using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public int? Quantity { get; set; }
        public int ProductId { get; set; }
        public int StorageMethodId { get; set; }

        public virtual Product Product { get; set; }
        public virtual StorageMethod StorageMethod { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
