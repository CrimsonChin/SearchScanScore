using CodeHunt.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeHunt.Api.Filters
{
    public class HttpResponseExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!(context.Exception is HttpResponseException exception))
            {
                return;
            }

            var payload = new JsonErrorPayload {StatusCode = exception.StatusCode, DetailedMessage = exception.Message};

            context.Result = new ObjectResult(payload) {StatusCode = exception.StatusCode};
            context.ExceptionHandled = true;
        }
    }
}
