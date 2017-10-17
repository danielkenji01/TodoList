using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Infraestructure;

namespace TodoList.Features.Assignment
{
    public class Update
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }
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
                var assignment = await db.Assignment.FindAsync(message.Id);

                if (message.Name != null) assignment.Name = message.Name;

                if (message.Description != null) assignment.Description = message.Description;

                await db.SaveChangesAsync();
            }
        }
    }
}