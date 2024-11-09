using System;
using System.Xml;

namespace RazorPagesApp.Middelwares;

/// <summary>
/// This middleware watch for not-authentication user and redirect them to 
/// authentication page.
/// </summary>
public class AuntificationMiddelware
{
    protected readonly RequestDelegate next;

    public const string AuthPath = "/";
    public const string AdminAuthPath = "/Administration/AdminAuthorize";

    public AuntificationMiddelware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (IsAuthenticationRedirection(context))
        {
            context.Session.SetString("authentication", "start");
            context.Response.Redirect(AuthPath);
        }
        else
        {
            await next.Invoke(context);
        }
    }
    protected bool IsAuthenticationRedirection(HttpContext context)
    {
        string AuthenticationType = String.Empty;

        if (!context.Session.Keys.Contains("authentication"))
            return true;

        AuthenticationType = context.Session.GetString("authentication")!;

        if (context.Request.Path == AdminAuthPath)
            return false;

        if(AuthenticationType == "end" || AuthenticationType == "admin")
            return false;

        if (AuthenticationType == "start" && context.Request.Path == AuthPath)
            return false;

        return true;
    }
}
