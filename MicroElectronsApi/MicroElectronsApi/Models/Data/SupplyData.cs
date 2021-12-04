using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroElectronsApi.Models.Data
{
    public class SupplyData
    {
        public int SupplyId { get; set; }
        public bool IsSell { get; set; }
        public string DateSupply { get; set; }
        public int CounterpartyId { get; set; }
        public string CounterpartyName { get; set; }
        public List<ProductData> Products { get; set; }
    }
}
