using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class EmployeeStatus
    {
        public EmployeeStatus()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
