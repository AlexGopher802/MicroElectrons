using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class OperationType
    {
        public OperationType()
        {
            ComeJournals = new HashSet<ComeJournal>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ComeJournal> ComeJournals { get; set; }
    }
}
