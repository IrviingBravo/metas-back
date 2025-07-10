using Microsoft.EntityFrameworkCore;
using metas.Models;

namespace metas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ActivityList> ActivityLists { get; set; }

        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.ActivityList)
                .WithMany(a => a.Tasks)
                .HasForeignKey(t => t.ActivityListId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
