﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Domain
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Boolean IsFinished { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public Guid AssignmentId { get; set; }

        #region Navigation

        public virtual Assignment Assignment { get; set; }

        #endregion Navigation
    }
}