using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PersonalExpenseTracker.Web.Filters
{
    public class RedirectIfAuthenticatedAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                // Redirect to a different controller or action if the user is authenticated
                context.Result = new RedirectToActionResult("Index", "User", null);
            }
            else
            {
                await next();
            }
        }
    }
}
