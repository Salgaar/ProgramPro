using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using ProgramPro.Server.Data;
using ProgramPro.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var identityConnectionstring = builder.Configuration.GetConnectionString("IdentityDb") ?? throw new InvalidOperationException("Connection string 'IdentityDb' not found.");
var programProConnectionstring = builder.Configuration.GetConnectionString("ProgramProDb") ?? throw new InvalidOperationException("Connection string 'ProgramProDb' not found.");

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(identityConnectionstring));
builder.Services.AddDbContext<ProgramProDbContext>(options =>
    options.UseSqlServer(programProConnectionstring));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, IdentityContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
