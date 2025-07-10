using System;
using metas.Data; 

namespace metas.Data
{
    public class TaskItemMigration
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public bool Completed { get; set; }

        public int ActivityListId { get; set; }
        public virtual ActivityListMigration ActivityListMigration { get; set; }
    }
}
