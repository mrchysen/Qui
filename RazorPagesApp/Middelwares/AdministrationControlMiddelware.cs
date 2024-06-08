namespace RazorPagesApp.Middelwares;

public class AdministrationControlMiddelware
{
    protected readonly RequestDelegate next;

    public AdministrationControlMiddelware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Value.Contains("/Administration"))
        {
            if(context.Request.Path != "/Administration/AdminAuthorize")
            {
                if(context.Session.GetString("authentication") != "admin")
                {
                    context.Response.Redirect("/Administration/AdminAuthorize");
                }
            }   
        }

        await next.Invoke(context);
    }
}
