using GlobalErrorHandling.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace GlobalErrorHandling.Extensions
{
    public class ValidateAttributeFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int id = Convert.ToInt32(context.ActionArguments["id"]);
            if (id <= 0)
            {
                throw new CustomBadRequest("Employee :" + id + " is not valid");
            }
        }
    }
}