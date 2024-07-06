using Microsoft.EntityFrameworkCore;
using portfolio.Data;
using portfolio.Github;
using portfolio.Interfaces;
using portfolio.Respository;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<GithubService>();
builder.Services.AddDbContext<DataContext>(options =>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHangfire(config => 
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    options.SlidingExpiration = false;
    options.LoginPath = "/Identity/Account/Login";
});

builder.Services.AddAuthorization();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 17;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequiredUniqueChars = 1;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();


builder.Services.AddHangfireServer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.UseCookiePolicy();

app.UseHangfireDashboard("/hangfire", new DashboardOptions {
    Authorization = new[] {new HangfireAuthorizationFilter()}
});


// Add a recurring job
#pragma warning disable CS0618 // Type or member is obsolete
RecurringJob.AddOrUpdate(
    "fetchgithub-data",
    () => new GithubService().FetchAndDisplayRepos("darth-tenebros"),
    "30 20 * * *", // TODO: FIND A MORE ELEGANT PLACE TO PUT THIS IN
    TimeZoneInfo.Local
);
#pragma warning restore CS0618 // Type or member is obsolete


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await Seed.SeedUsersAndRolesAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}


app.Run();
