using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Labs.WebApiApp.Filters
{
    public class MyExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(new { error = context.Exception.Message })
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }
    }
}
