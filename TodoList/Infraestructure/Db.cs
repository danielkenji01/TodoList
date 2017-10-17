using Microsoft.EntityFrameworkCore;
using TodoList.Domain;

namespace TodoList.Infraestructure
{
    public class Db : DbContext
    {
        #region Tables

        public DbSet<Assignment> Assignment { get; set; }

        public DbSet<Item> Item { get; set; }

        #endregion Tables

        public Db(DbContextOptions<Db> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder m)
        {
            base.OnModelCreating(m);

            m.Entity<Item>().ToTable(nameof(Item));
            m.Entity<Assignment>().ToTable(nameof(Assignment));

            m.Entity<Assignment>().HasMany(t => t.Itens).WithOne(t => t.Assignment).HasForeignKey(t => t.AssignmentId);
            m.Entity<Assignment>().Property(t => t.CreatedAt).ValueGeneratedOnAdd();
            m.Entity<Assignment>().Property(t => t.UpdatedAt).ValueGeneratedOnAddOrUpdate();

            m.Entity<Item>().HasOne(i => i.Assignment).WithMany(i => i.Itens).HasForeignKey(i => i.AssignmentId);
            m.Entity<Item>().Property(t => t.CreatedAt).ValueGeneratedOnAdd();
            m.Entity<Item>().Property(t => t.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        }
    }
}