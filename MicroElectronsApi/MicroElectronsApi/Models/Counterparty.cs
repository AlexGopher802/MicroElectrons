using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class Counterparty
    {
        public Counterparty()
        {
            Supplies = new HashSet<Supply>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Tin { get; set; }
        public string Address { get; set; }
        public string Bic { get; set; }

        public virtual ICollection<Supply> Supplies { get; set; }
    }
}
