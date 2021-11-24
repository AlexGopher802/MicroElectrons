using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroElectronsApi.Models.Data
{
    public class UserData
    {
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Birthday { get; set; }
        public string Status { get; set; }
        public string Post { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public double Salary { get; set; }
        public string LaborDate { get; set; }
    }
}
