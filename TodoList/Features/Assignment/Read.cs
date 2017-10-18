using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Infraestructure;

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

            public SubResult[] Itens { get; set; }

            public class SubResult
            {
                public Guid Id { get; set; }

                public string Name { get; set; }

                public string Description { get; set; }

                public Boolean IsFinished { get; set; }
            }
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
                var assignment = await db.Assignment
                    .Include(a => a.Assignment_Item)
                    .ThenInclude(ai => ai.Item)
                    .SingleOrDefaultAsync(a => a.Id == message.Id);

                if (assignment == null || assignment.DeletedAt.HasValue) throw new NotFoundException();

                return new Result
                {
                    Id = assignment.Id,
                    Name = assignment.Name,
                    Description = assignment.Description,
                    CreatedAt = assignment.CreatedAt,
                    Itens = assignment.Assignment_Item?.Where(ai => !ai.Item.DeletedAt.HasValue).Select(a => new Result.SubResult
                    {
                        Id = a.Item.Id,
                        Name = a.Item.Name,
                        Description = a.Item.Description,
                        IsFinished = a.Item.IsFinished
                    }).ToArray()
                };
            }
        }
    }
}