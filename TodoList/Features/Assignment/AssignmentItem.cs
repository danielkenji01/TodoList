using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Infraestructure;

namespace TodoList.Features.Assignment
{
    public class AssignmentItem
    {
        public class Command : IRequest
        {
            public Guid AssignmentId { get; set; }

            public Guid ItemId { get; set; }
        }

        public class CommandHandler : IAsyncRequestHandler<Command>
        {
            private readonly Db db;

            public CommandHandler(Db db)
            {
                this.db = db;
            }

            public async Task Handle(Command message)
            {
                if (db.Assignment.SingleOrDefault(a => a.Id == message.AssignmentId) == null) throw new NotFoundException();

                if (db.Item.SingleOrDefault(a => a.Id == message.ItemId) == null) throw new NotFoundException();

                if (db.Assignment_Item.Any(a => a.ItemId == message.ItemId && a.AssignmentId == message.AssignmentId)) return;

                var assignmentItem = new Domain.Assignment_Item()
                {
                    AssignmentId = message.AssignmentId,
                    ItemId = message.ItemId
                };

                db.Assignment_Item.Add(assignmentItem);

                await db.SaveChangesAsync();
            }
        }
    }
}