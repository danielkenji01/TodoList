using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Infraestructure;
using static TodoList.Infraestructure.HttpException;

namespace TodoList.Features.Assignment
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

            public string Name { get; set; }

            public string Description { get; set; }

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
                var assignment = await db.Assignment.FindAsync(message.Id);

                if (assignment == null) throw new NotFoundException();

                return new Result
                {
                    Id = assignment.Id,
                    Name = assignment.Name,
                    Description = assignment.Description,
                    CreatedAt = assignment.CreatedAt
                };
            }
        }
    }
}