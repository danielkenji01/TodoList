using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Infraestructure;

namespace TodoList.Features.Item
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid AssignmentId { get; set; }

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
                var item = new Domain.Item()
                {
                    Name = message.Name,
                    Description = message.Description,
                    IsFinished = message.IsFinished,
                    AssignmentId = message.AssignmentId
                };

                db.Item.Add(item);

                await db.SaveChangesAsync();
            }
        }
    }
}