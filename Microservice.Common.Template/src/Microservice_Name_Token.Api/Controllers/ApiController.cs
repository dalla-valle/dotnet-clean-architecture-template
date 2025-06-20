namespace Microservice_Name_Token.Controllers;

using System.Net;
using ErrorOr;
using Microservice_Name_Token.Api.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Base API controller providing custom validation and error handling.
/// </summary>
[TypeFilter(typeof(InputValidationActionFilter))]
public class ApiController : ControllerBase
{
    /// <summary>
    /// Returns a custom validation problem response.
    /// </summary>
    /// <param name="modelStateDictionary">The model state dictionary containing validation errors.</param>
    /// <returns>An <see cref="ActionResult"/> representing the validation problem.</returns>
    public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary) =>
        new ObjectResult(CustomProblemDetails.Build(modelStateDictionary))
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
        };

    /// <summary>
    /// Returns a problem response based on a list of errors.
    /// </summary>
    /// <param name="errors">The list of errors to process.</param>
    /// <returns>An <see cref="IActionResult"/> representing the problem response.</returns>
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.All(e => e.Type == ErrorType.Validation))
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(error.Code, error.Description);
            }

            return this.ValidationProblem(modelStateDictionary);
        }

        if (errors.Any(e => e.Type == ErrorType.Unexpected))
        {
            return this.Problem();
        }

        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return this.Problem(statusCode: statusCode, title: firstError.Description);
    }
}