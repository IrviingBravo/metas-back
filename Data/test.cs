using System;
using System.Collections.Generic;
using System.Linq; 
using metas.Data;

namespace metas.Data
{
    public class ActivityListMigration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual List<TaskItemMigration> Tasks { get; set; } = new();

        public int PorcentCompleted =>
            Tasks == null || Tasks.Count == 0 ? 0 :
            (int)((Tasks.Count(t => t.Completed) / (double)Tasks.Count) * 100);
    }
}
