using Azure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ModuleAPI.Filters
{
    public class RequestFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Este método é executado antes da execução do método de ação
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:4200");
        }
    }
}
