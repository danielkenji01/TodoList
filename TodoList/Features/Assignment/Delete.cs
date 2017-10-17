using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Infraestructure;

namespace TodoList.Features.Assignment
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Command>
        {
            private readonly Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            public async Task Handle(Command message)
            {
                var assignment = await db.Assignment.FindAsync(message.Id);

                assignment.DeletedAt = DateTime.Now;

                await db.SaveChangesAsync();
            }
        }
    }
}