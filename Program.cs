using Microsoft.EntityFrameworkCore;
using portfolio.Data;
using portfolio.Github;
using portfolio.Interfaces;
using portfolio.Respository;
using Hangfire;

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

app.UseAuthorization();

app.MapRazorPages();

app.UseHangfireDashboard();


// Add a recurring job
#pragma warning disable CS0618 // Type or member is obsolete
RecurringJob.AddOrUpdate(
    "fetchgithub-data",
    () => new GithubService().FetchAndDisplayRepos("darth-tenebros"),
    "30 20 * * *", // TODO: FIND A MORE ELEGANT PLACE TO PUT THIS IN
    TimeZoneInfo.Local
);
#pragma warning restore CS0618 // Type or member is obsolete

app.Run();
