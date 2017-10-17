using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TodoList.Infraestructure
{
    public class Middleware
    {
        private readonly RequestDelegate next;

        public Middleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (HttpException e)
            {
                context.Response.StatusCode = e.StatusCode;
                if (e.Body != null)
                {
                }
            }
        }
    }
}