using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Domain
{
    public class Assignment_Item
    {
        public Guid AssignmentId { get; set; }

        public Guid ItemId { get; set; }

        #region Navigation

        public virtual Assignment Assignment { get; set; }

        public virtual Item Item { get; set; }

        #endregion Navigation
    }
}