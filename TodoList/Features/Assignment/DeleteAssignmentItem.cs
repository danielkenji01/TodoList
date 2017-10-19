using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Infraestructure;

namespace TodoList.Features.Assignment
{
    public class DeleteAssignmentItem
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
                var assignment = db.Assignment
                    .Include(a => a.Assignment_Item)
                    .SingleOrDefault(a => a.Id == message.AssignmentId);

                if (assignment == null) throw new NotFoundException();

                if (!assignment.Assignment_Item.Any(a => a.ItemId == message.ItemId)) throw new NotFoundException();

                db.Assignment_Item.RemoveRange(assignment.Assignment_Item.Where(ai => ai.ItemId == message.ItemId));

                await db.SaveChangesAsync();
            }
        }
    }
}