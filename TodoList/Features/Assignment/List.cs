using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Infraestructure;

namespace TodoList.Features.Assignment
{
    public class List
    {
        public class Query : IRequest<IList<Result>>
        {
        }

        public class Result
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

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
                return await db.Assignment
                    .Where(a => a.DeletedAt == null)
                    .Select(a => new Result
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Description = a.Description,
                        CreatedAt = a.CreatedAt
                    }).ToListAsync();
            }
        }
    }
}