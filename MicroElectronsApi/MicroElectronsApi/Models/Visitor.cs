using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class Visitor
    {
        public Visitor()
        {
            VisitorJournals = new HashSet<VisitorJournal>();
        }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Passport { get; set; }

        public virtual ICollection<VisitorJournal> VisitorJournals { get; set; }
    }
}
