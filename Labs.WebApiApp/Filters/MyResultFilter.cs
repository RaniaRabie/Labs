using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Labs.WebApiApp.Filters
{
    public class MyResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var newResponse = new
                {
                    success = true,
                    data = objectResult.Value
                };

                objectResult.Value = newResponse;
            }
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            
        }
    }
}
