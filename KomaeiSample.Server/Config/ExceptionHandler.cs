namespace KomaeiSample.Server.Config;
public static class ExceptionHandler
{
    public static void AddExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature?.Error is AppException)
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsync(CheckException(contextFeature!.Error));
            });
        });
    }
    private static string CheckException(Exception ex)
    {
        if (ex.InnerException is not null)
            return ex.Message + " - InnerException : " + ex.InnerException.Message;
        return ex.Message;
    }
}

public class AppException : Exception
{
    public AppException(string message) : base(message) { }
}