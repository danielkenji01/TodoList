using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Infraestructure;

namespace TodoList.Features.Item
{
    public class Update
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public Boolean IsFinished { get; set; }
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
                var item = await db.Item.FindAsync(message.Id);

                if (message.Name != null) item.Name = message.Name;

                if (message.IsFinished.ToString() != null) item.IsFinished = message.IsFinished;

                if (message.Description != null) item.Description = message.Description;

                await db.SaveChangesAsync();
            }
        }
    }
}