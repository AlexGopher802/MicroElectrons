using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class VisitorJournal
    {
        public int Id { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public DateTime? DateTimeExit { get; set; }
        public int EmployeeEntryId { get; set; }
        public int? EmployeeExitId { get; set; }
        public int VisitorId { get; set; }

        public virtual Employee EmployeeEntry { get; set; }
        public virtual Employee EmployeeExit { get; set; }
        public virtual Visitor Visitor { get; set; }
    }
}
