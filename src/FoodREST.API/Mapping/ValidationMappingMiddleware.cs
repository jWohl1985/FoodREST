using FoodREST.API.Responses;
using FluentValidation;

namespace FoodREST.API.Mapping;

public class ValidationMappingMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationMappingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            context.Response.StatusCode = 400;

            var validationFailureResponse = new ValidationFailureResponse
            {
                Errors = exception.Errors.Select(error => new ValidationResponse
                {
                    PropertyName = error.PropertyName,
                    Message = error.ErrorMessage
                })
            };

            await context.Response.WriteAsJsonAsync(validationFailureResponse);
        }
    }
}
