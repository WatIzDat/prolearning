namespace ProLearning.Api.ApiKey;

public class ApiKeyEndpointFilter(IApiKeyValidator apiKeyValidator) : IEndpointFilter
{
    private readonly IApiKeyValidator apiKeyValidator = apiKeyValidator;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        string apiKey = context.HttpContext.Request.Query["apiKey"].ToString();
        
        if (string.IsNullOrWhiteSpace(apiKey))
            return Results.BadRequest();

        if (!apiKeyValidator.IsValid(apiKey))
            return Results.Unauthorized();
        
        return await next(context);
    }
}