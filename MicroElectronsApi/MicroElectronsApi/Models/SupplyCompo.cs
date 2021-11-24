using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class SupplyCompo
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Summa { get; set; }
        public int ProductId { get; set; }
        public int SupplyId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Supply Supply { get; set; }
    }
}
