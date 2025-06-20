namespace Microservice_Name_Token.Api.Mapper;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

/// <summary>
/// Action filter that validates the input model state before executing an action.
/// </summary>
public class InputValidationActionFilter : IActionFilter
{
    /// <summary>
    /// Called before the action executes, to validate the model state.
    /// </summary>
    /// <param name="context">The context for the action executing.</param>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new ObjectResult(CustomProblemDetails.Build(context.ModelState))
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
            };
        }
    }

    /// <summary>
    /// Called after the action executes.
    /// </summary>
    /// <param name="context">The context for the action executed.</param>
    public void OnActionExecuted(ActionExecutedContext context)
    { }
}