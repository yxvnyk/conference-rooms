using ConferenceRooms.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ConferenceRooms.WebApi.Middlewares
{
	public class ExceptionHandlingMiddleware : IMiddleware
	{
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;

		public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
		{
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while processing the request.");
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			if (ex is AggregateException aggregateException)
			{
				ex = aggregateException.InnerException ?? ex;
			}

			// Get PostgresException directly, or from InnerException
			var pgEx = ex as PostgresException ?? ex.InnerException as PostgresException;

			if (pgEx != null)
			{
				await HandlePostgresException(context, pgEx);
				return;
			}

			switch (ex)
			{
				// catching all our HallIsNotAvailableException, HallNotExistException etc.
				case BaseCustomlException apiEx:
					await HandCustomExceptionAsync(context, apiEx);
					break;
				case TimeoutException:
					await HandleTimeoutException(context);
					break;
				case DbUpdateException dbUpdateEx:
					await HandlDBUpdatelException(context, dbUpdateEx.Message);
					break;
				default:
					await UnknownExceptionHandler(context);
					break;
			}
		}

		private static async Task HandlePostgresException(HttpContext context, PostgresException pgEx)
		{
			switch (pgEx.SqlState)
			{
				case "23505": // Unique constraint violation 
					await HandleUniquePostgresException(context, pgEx.ConstraintName);
					break;

				case "42501": // Insufficient privilege
					context.Response.StatusCode = 403;
					await context.Response.WriteAsJsonAsync(new ProblemDetails
					{
						Title = "Forbidden",
						Detail = "У вас немає прав для виконання цієї операції в базі даних.",
						Status = 403
					});
					break;

				default:
					context.Response.StatusCode = 500;
					await context.Response.WriteAsJsonAsync(new ProblemDetails
					{
						Title = "Database error",
						Detail = "A database error has occurred.",
						Status = 500
					});
					break;
			}
		}

		private static async Task HandCustomExceptionAsync(HttpContext context, BaseCustomlException exception)
		{
			var details = new ProblemDetails()
			{
				Detail = exception.Message,
				Title = exception.ErrorCode, 
				Status = exception.StatusCode,
			};

			context.Response.StatusCode = exception.StatusCode;
			await context.Response.WriteAsJsonAsync(details);
		}

		private static async Task HandleTimeoutException(HttpContext context)
		{
			var details = new ProblemDetails()
			{
				Detail = "The request timed out.",
				Title = "Timeout error",
				Status = 504,
			};

			context.Response.StatusCode = 504;
			await context.Response.WriteAsJsonAsync(details);
		}

		private static async Task HandlDBUpdatelException(HttpContext context, string text)
		{
			var details = new ProblemDetails()
			{
				Detail = text,
				Title = "Database Update Error",
				Status = 409,
			};

			context.Response.StatusCode = 409;
			await context.Response.WriteAsJsonAsync(details);
		}

		private static async Task HandleUniquePostgresException(HttpContext context, string constraintName)
		{
			var newText = constraintName switch
			{
				"IX_halls_name" => "Hall with such name already exist. Choose another name.",
				_ => "Data with similar has already exist."
			};

			var details = new ProblemDetails()
			{
				Detail = newText,
				Title = "Not unique data",
				Status = 409,
			};

			context.Response.StatusCode = 409;
			await context.Response.WriteAsJsonAsync(details);
		}

		private static async Task UnknownExceptionHandler(HttpContext context)
		{
			context.Response.StatusCode = 500;
			await context.Response.WriteAsJsonAsync(new ProblemDetails
			{
				Title = "Internal Server Error",
				Detail = "An unexpected error occurred.",
				Status = 500,
			});
		}
	}
}
