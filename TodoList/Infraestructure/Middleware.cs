using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TodoList.Infraestructure
{
    public class Middleware
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
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