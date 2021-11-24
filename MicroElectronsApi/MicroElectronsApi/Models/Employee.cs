using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Dismissals = new HashSet<Dismissal>();
            Holidays = new HashSet<Holiday>();
            Labors = new HashSet<Labor>();
            Tasks = new HashSet<Task>();
            Users = new HashSet<User>();
            VisitorJournalEmployeeEntries = new HashSet<VisitorJournal>();
            VisitorJournalEmployeeExits = new HashSet<VisitorJournal>();
        }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public int PostId { get; set; }
        public int StatusId { get; set; }

        public virtual Post Post { get; set; }
        public virtual EmployeeStatus Status { get; set; }
        public virtual ICollection<Dismissal> Dismissals { get; set; }
        public virtual ICollection<Holiday> Holidays { get; set; }
        public virtual ICollection<Labor> Labors { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<VisitorJournal> VisitorJournalEmployeeEntries { get; set; }
        public virtual ICollection<VisitorJournal> VisitorJournalEmployeeExits { get; set; }
    }
}
