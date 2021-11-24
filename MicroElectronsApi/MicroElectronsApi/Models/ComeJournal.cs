using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class ComeJournal
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTimeConfirm { get; set; }
        public bool? IsCome { get; set; }
        public int OperationId { get; set; }

        public virtual OperationType Operation { get; set; }
    }
}
