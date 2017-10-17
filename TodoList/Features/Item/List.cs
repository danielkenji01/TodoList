using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Infraestructure;

namespace TodoList.Features.Item
{
    public class List
    {
        public class Query : IRequest<IList<Result>>
        {
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

        public class QueryHandler : IAsyncRequestHandler<Query, IList<Result>>
        {
            private readonly Db db;

            public QueryHandler(Db db)
            {
                this.db = db;
            }

            public async Task<IList<Result>> Handle(Query message)
            {
                return await db.Item
                    .Where(i => i.DeletedAt == null)
                    .Select(i => new Result
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Description = i.Description,
                        IsFinished = i.IsFinished,
                        CreatedAt = i.CreatedAt,
                        AssignmentId = i.AssignmentId
                    }).ToListAsync();
            }
        }
    }
}