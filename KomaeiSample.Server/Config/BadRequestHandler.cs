namespace KomaeiSample.Server.Config;
public static class BadRequestHandler
{
    public static IMvcBuilder AddBadRequestHandler(this IMvcBuilder services)
    {
        services.ConfigureApiBehaviorOptions(options =>
        options.InvalidModelStateResponseFactory = actionContext =>
        {
            var modelState = actionContext.ModelState.Values;
            var allErrors = actionContext.ModelState.Values.SelectMany(v => v.Errors);
            return new BadRequestObjectResult(new
            {
                StatusCode = 400,
                Message = string.Join(" - ", allErrors.Select(e => e.ErrorMessage))
            });
        });
        return services;
    }
}