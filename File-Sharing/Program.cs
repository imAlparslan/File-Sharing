using File_Sharing.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddScoped<File_Sharing.Services.IUser, UserRepository>();
builder.Services.AddScoped<File_Sharing.Services.IDocument, DocumentRepository>();
builder.Services.AddScoped<File_Sharing.Services.IFriendship, FriendshipRepository>();
builder.Services.AddScoped<File_Sharing.Services.IDocumentAccess, DocumentAccessRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie
    (x =>
    {
        x.Cookie.Name = "CookieAuth";
        x.LoginPath = "/login";
        x.AccessDeniedPath = ("/home/AccessDenied");
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserPolicy", policy => policy.RequireClaim("type", "User"));
    options.AddPolicy("UserRole", policy => policy.RequireRole("User"));
    
});

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"), b => b.MigrationsAssembly("File-Sharing"));
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 1073741824;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
