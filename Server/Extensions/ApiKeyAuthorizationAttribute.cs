using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAuthorizationAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _apiKey = "ProgramPro_#DONT#TOUCH#THIS.IS^^VERY**-12394827523235123.23.423,2134#RESTRICTED_API.Key";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var request = context.HttpContext.Request;
        var providedApiKey = request.Headers["ApiKey"];

        if (!string.IsNullOrEmpty(providedApiKey) && providedApiKey == _apiKey)
        {
            return; // API key is valid
        }

        // If the API key is not valid, return a 401 Unauthorized response.
        context.Result = new UnauthorizedResult();
    }
}
