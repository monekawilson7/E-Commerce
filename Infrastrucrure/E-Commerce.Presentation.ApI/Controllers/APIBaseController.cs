global using E_Commerce.ServiceAbstraction.Common;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using System.Reflection;

namespace E_Commerce.Presentation.ApI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class APIBaseController : ControllerBase
{
    protected IActionResult HandelResult(Result result)
    {
        if (result.IsSuccess)
            return NoContent();
        return Problem(result.Errors);
    }

    private IActionResult Problem(IReadOnlyList<Error> errors)
    {
        if (errors.Count ==0)
            return Problem(statusCode: 500, title:"An unexpected error occured." );
        if (errors.All(e => e.Type == ErrorType.Validation))
            return HandleValidationProblem(errors);
        return HandelSingleErrorProblem(errors[0]);
    }

    private IActionResult HandelSingleErrorProblem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        return Problem(statusCode: statusCode, title: error.Description,type: error.Code );
    }

    private IActionResult HandleValidationProblem(IReadOnlyList<Error> errors)
    {
       var modelState = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelState.AddModelError(error.Code, error.Description);
        }
        return ValidationProblem(modelState);
    }

    protected ActionResult<TValue> HandelResult<TValue>(Result<TValue> result)
    {
        if (result.IsSuccess)
        {
            if (result.Value == null)
                return new NoContentResult();

            return new OkObjectResult(result.Value);
        }

        return new ObjectResult(result.Errors)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}
