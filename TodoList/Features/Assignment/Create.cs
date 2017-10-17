using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using TodoList.Infraestructure;

namespace TodoList.Features.Assignment
{
    public class Create
    {
        public class Command : IRequest
        {
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
                var assignment = new Domain.Assignment()
                {
                    Name = message.Name,
                    Description = message.Description
                };

                db.Assignment.Add(assignment);

                await db.SaveChangesAsync();
            }
        }
    }
}