
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.GlobalException
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ProblemDetails porb = new()
            {
                Title = "smth error",
                Detail =exception.Message,
                Status = 404,
                
            };
            porb.Extensions.Add("Signiture","Yousef Maawww");


            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
           await httpContext.Response.WriteAsJsonAsync(porb, cancellationToken);

            return true;
        }
    }
}
