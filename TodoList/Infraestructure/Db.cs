using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Domain;

namespace TodoList.Infraestructure
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder m)
        {
            base.OnModelCreating(m);

            m.Entity<Item>().ToTable(nameof(Item));
            m.Entity<Domain.Task>().ToTable(nameof(Domain.Task));

            m.Entity<Domain.Task>().HasMany(t => t.Itens).WithOne(t => t.Task).HasForeignKey(t => t.TaskId);
            m.Entity<Domain.Task>().Property(t => t.CreatedAt).ValueGeneratedOnAdd();
            m.Entity<Domain.Task>().Property(t => t.UpdatedAt).ValueGeneratedOnAddOrUpdate();

            m.Entity<Item>().HasOne(i => i.Task).WithMany(i => i.Itens).HasForeignKey(i => i.TaskId);
            m.Entity<Item>().Property(t => t.CreatedAt).ValueGeneratedOnAdd();
            m.Entity<Item>().Property(t => t.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        }
    }
}