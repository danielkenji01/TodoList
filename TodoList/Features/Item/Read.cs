using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Infraestructure;

namespace TodoList.Features.Item
{
    public class Read
    {
        public class Query : IRequest<Result>
        {
            public Guid Id { get; set; }
        }

        public class Result
        {
            public Guid Id { get; set; }

            public Guid AssignmentId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public Boolean IsFinished { get; set; }

            public DateTime CreatedAt { get; set; }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, Result>
        {
            private readonly Db db;

            public QueryHandler(Db db)
            {
                this.db = db;
            }

            public async Task<Result> Handle(Query message)
            {
                var item = await db.Item.FindAsync(message.Id);

                return new Result
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    IsFinished = item.IsFinished,
                    CreatedAt = item.CreatedAt,
                    AssignmentId = item.AssignmentId
                };
            }
        }
    }
}