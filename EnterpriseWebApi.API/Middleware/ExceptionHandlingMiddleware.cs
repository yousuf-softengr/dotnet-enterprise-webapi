using EnterpriseWebApi.API.Errors;
using EnterpriseWebApi.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace EnterpriseWebApi.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            ProblemDetails problem;

            switch (ex)
            {
                case NotFoundException:
                    problem = ApiProblemDetailsFactory.Create(
                        context,
                        StatusCodes.Status404NotFound,
                        "Resource not found",
                        ex.Message);
                    break;

                case BadRequestException:
                    problem = ApiProblemDetailsFactory.Create(
                        context,
                        StatusCodes.Status400BadRequest,
                        "Bad request",
                        ex.Message);
                    break;

                case ConflictException:
                    problem = ApiProblemDetailsFactory.Create(
                        context,
                        StatusCodes.Status409Conflict,
                        "Conflict",
                        ex.Message);
                    break;

                default:
                    problem = ApiProblemDetailsFactory.Create(
                        context,
                        StatusCodes.Status500InternalServerError,
                        "Internal server error",
                        "An unexpected error occurred");
                    break;
            }

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problem.Status!.Value;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(problem));
        }

    }
}
