using Microsoft.EntityFrameworkCore;
using TodoList.Domain;

namespace TodoList.Infraestructure
{
    public class Db : DbContext
    {
        #region Tables

        public DbSet<Assignment> Assignment { get; set; }

        public DbSet<Item> Item { get; set; }

        public DbSet<Assignment_Item> Assignment_Item { get; set; }

        #endregion Tables

        public Db(DbContextOptions<Db> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder m)
        {
            base.OnModelCreating(m);

            m.Entity<Item>().ToTable(nameof(Item));
            m.Entity<Assignment>().ToTable(nameof(Assignment));
            m.Entity<Assignment_Item>().ToTable(nameof(Assignment_Item));

            m.Entity<Assignment>().Property(t => t.CreatedAt).ValueGeneratedOnAdd();
            m.Entity<Assignment>().Property(t => t.UpdatedAt).ValueGeneratedOnAddOrUpdate();

            m.Entity<Item>().Property(t => t.CreatedAt).ValueGeneratedOnAdd();
            m.Entity<Item>().Property(t => t.UpdatedAt).ValueGeneratedOnAddOrUpdate();

            m.Entity<Assignment_Item>().HasKey(ai => new { ai.AssignmentId, ai.ItemId }).ForSqlServerIsClustered(true);
        }
    }
}