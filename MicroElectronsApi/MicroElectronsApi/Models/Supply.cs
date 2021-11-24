using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class Supply
    {
        public Supply()
        {
            SupplyCompos = new HashSet<SupplyCompo>();
        }

        public int Id { get; set; }
        public bool? IsSell { get; set; }
        public DateTime DateSupply { get; set; }
        public int CounterpartyId { get; set; }

        public virtual Counterparty Counterparty { get; set; }
        public virtual ICollection<SupplyCompo> SupplyCompos { get; set; }
    }
}
