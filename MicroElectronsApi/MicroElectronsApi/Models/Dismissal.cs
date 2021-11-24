using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class Dismissal
    {
        public int Id { get; set; }
        public DateTime DateConfirm { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
