using EmployeeManagementSystem.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace EmployeeManagementSystem.WebApp.AppFilter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AppFilterController : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new AppFilterImplementation(
                serviceProvider.GetRequiredService<TokenValidator>()
            );
        }

        private class AppFilterImplementation : IActionFilter
        {
            private readonly TokenValidator _tokenValidator;

            public AppFilterImplementation(TokenValidator tokenValidator)
            {
                _tokenValidator = tokenValidator;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                var token = context.HttpContext.Session.GetString("AuthToken");

                if (!_tokenValidator.IsTokenValid(token))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        { "action", "index" },
                        { "controller", "Login" }
                    });
                }
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                // Implement if needed
            }
        }
    }
}