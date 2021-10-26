using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OzonEdu.MerchendiseService.Infrastructure.Filters.Models;

namespace OzonEdu.MerchendiseService.Infrastructure.Filters
{
    public sealed class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exceptionInfo = new ExceptionResponseModel
            {
                Message = context.Exception.Message,
                ExceptionType = context.Exception.GetType().FullName,
                StackTrace = context.Exception.StackTrace
            };

            context.Result = new JsonResult(exceptionInfo)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}