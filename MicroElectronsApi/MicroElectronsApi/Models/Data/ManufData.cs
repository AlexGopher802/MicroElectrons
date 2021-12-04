using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroElectronsApi.Models.Data
{
    public class ManufData
    {
        public int TaskId { get; set; }
        public string DateStart { get; set; }
        public string DateDeadline { get; set; }
        public string DateEnd { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int EmployeeId { get; set; }
        public string Status { get; set; }
    }
}
