using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroElectronsApi.Models.Data
{
    public class ComeJournalData
    {
        public int JournalId { get; set; }
        public string SubjectName { get; set; }
        public int Quantity { get; set; }
        public string DateTimeConfirm { get; set; }
        public string IsCome { get; set; }
        public string Operation { get; set; }
    }
}
