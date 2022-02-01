using GraduateProject.services;

namespace GraduateProject.utils;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = JwtUtils.ValidateToken(token);
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = userService.GetById(userId.Value);
        }

        await _next(context);
    }
}