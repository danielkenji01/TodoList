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

            public SubResult[] Itens { get; set; }

            public class SubResult
            {
                public Guid Id { get; set; }

                public string Name { get; set; }

                public string Description { get; set; }

                public Boolean IsFinished { get; set; }
            }
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
                    .Include(a => a.Assignment_Item)
                    .ThenInclude(ai => ai.Item)
                    .Where(a => a.DeletedAt == null)
                    .Select(a => new Result
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Description = a.Description,
                        CreatedAt = a.CreatedAt,
                        Itens = a.Assignment_Item.Where(ai => !ai.Item.DeletedAt.HasValue).Select(i => new Result.SubResult
                        {
                            Id = i.Item.Id,
                            Name = i.Item.Name,
                            Description = i.Item.Description,
                            IsFinished = i.Item.IsFinished
                        }).ToArray()
                    }).ToListAsync();
            }
        }
    }
}