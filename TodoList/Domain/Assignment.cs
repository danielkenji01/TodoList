using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Domain
{
    public class Assignment
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        #region Navigation

        public virtual ICollection<Assignment_Item> Assignment_Item { get; set; }

        [NotMapped]
        public virtual IQueryable<Item> Itens => Assignment_Item?.Select(ai => ai.Item).AsQueryable();

        #endregion Navigation
    }
}