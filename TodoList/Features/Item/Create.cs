using MediatR;
using System;
using System.Threading.Tasks;
using TodoList.Infraestructure;

namespace TodoList.Features.Item
{
    public class Create
    {
        public class Command : IRequest
        {
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
                if (message.Name == null || message.Name.Trim().Equals("")) throw new HttpException(400);

                if (message.Description == null || message.Description.Trim().Equals("")) throw new HttpException(400);

                var item = new Domain.Item()
                {
                    Name = message.Name.Trim(),
                    Description = message.Description.Trim(),
                    IsFinished = message.IsFinished
                };

                db.Item.Add(item);

                await db.SaveChangesAsync();
            }
        }
    }
}