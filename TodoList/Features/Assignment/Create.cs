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
                if (message.Description == null || message.Description.Trim().Equals("")) throw new HttpException(400);

                if (message.Name == null || message.Name.Trim().Equals("")) throw new HttpException(400);

                var assignment = new Domain.Assignment()
                {
                    Name = message.Name.Trim(),
                    Description = message.Description.Trim()
                };

                db.Assignment.Add(assignment);

                await db.SaveChangesAsync();
            }
        }
    }
}