using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronsManager.Models
{
    public class HolidayData
    {
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Post { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
    }
}
