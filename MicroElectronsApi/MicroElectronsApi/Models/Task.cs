using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class Task
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime DateDeadline { get; set; }
        public int Quantity { get; set; }
        public int WarehouseId { get; set; }
        public int EmployeeId { get; set; }
        public int StatusId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual TaskStatus Status { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
