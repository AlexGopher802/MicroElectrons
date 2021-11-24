using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class Labor
    {
        public int Id { get; set; }
        public double Salary { get; set; }
        public DateTime DateConfirm { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
