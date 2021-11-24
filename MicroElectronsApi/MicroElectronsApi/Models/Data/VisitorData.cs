using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroElectronsApi.Models.Data
{
    public class VisitorData
    {
        public int EmployeeEntryId { get; set; }
        public string EmployeeEntryName { get; set; }
        public int? EmployeeExitId { get; set; }
        public string EmployeeExitName { get; set; }
        public string DateTimeEntry { get; set; }
        public string DateTimeExit { get; set; }
        public string VisitorLastName { get; set; }
        public string VisitorFirstName { get; set; }
        public string VisitorPatronymic { get; set; }
        public string Passport { get; set; }
    }
}
