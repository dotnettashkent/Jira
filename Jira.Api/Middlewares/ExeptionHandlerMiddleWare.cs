using Jira.Service.Exceptions;

namespace Jira.Api.Middlewares
{
    public class ExeptionHandlerMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExeptionHandlerMiddleWare> logger;
        public ExeptionHandlerMiddleWare(RequestDelegate next, ILogger<ExeptionHandlerMiddleWare> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (JiraException exception)
            {
                context.Response.StatusCode = exception.Code;
                await context.Response.WriteAsJsonAsync(new
                {
                    Code = exception.Code,
                    Error = exception.Message
                });
            }
            catch (Exception exception)
            {
                this.logger.LogError($"{exception.ToString()}\n");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    Code = 500,
                    Error = exception.Message,
                });
            }
        }
    }
}
