using Microsoft.IdentityModel.JsonWebTokens;
using Nasa.BLL.ServicesContracts;

namespace Nasa.API.Middlewares;

public class CurrentUserMiddleware
{
    private readonly RequestDelegate _next;

    public CurrentUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserIdSetter userIdSetter)
    {
        Console.WriteLine(context.User.Claims);
        var id = context.User.Claims.FirstOrDefault()?.Value;

        if (id is not null && int.TryParse(id, out var userId))
        {
            userIdSetter.SetCurrentUserId(userId);
        }

        await _next.Invoke(context);
    }
}