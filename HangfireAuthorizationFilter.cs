

using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authorization;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{

    public bool Authorize(DashboardContext context)
    {
        // Check if the user is authenticated
        return context.GetHttpContext().User.Identity.IsAuthenticated;
    }
}
