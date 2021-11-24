using System;
using System.Collections.Generic;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class TaskStatus
    {
        public TaskStatus()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
