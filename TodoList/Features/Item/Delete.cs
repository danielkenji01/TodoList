using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Infraestructure;

namespace TodoList.Features.Item
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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

                if (item == null || item.DeletedAt.HasValue) throw new NotFoundException();

                item.DeletedAt = DateTime.Now;

                await db.SaveChangesAsync();
            }
        }
    }
}