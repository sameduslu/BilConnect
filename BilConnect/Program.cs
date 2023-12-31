using BilConnect.Data;
using Microsoft.EntityFrameworkCore;
using Bilconnect_First_Version.data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using BilConnect.Data.Services;
using BilConnect.Data.Services.PostServices;
using BilConnect.Data.Services.ReportServices;
using BilConnect.Hubs;
using System;
using BilConnect.Models;

var builder = WebApplication.CreateBuilder(args);

// Add SignalR.
builder.Services.AddSignalR();
builder.Services.AddCors(options=>{
    options.AddDefaultPolicy(
        builder => {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
    );
});

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Configure the database context with the SQL Server
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnectionString")
));

//Services Configuration
builder.Services.AddScoped<IPostFactory, PostFactory>();
builder.Services.AddScoped<IPostsService, PostsService>();
builder.Services.AddScoped<IApplicationUsersService, ApplicationUsersService>();
builder.Services.AddScoped<IPostReportsService, PostReportsService>();
builder.Services.AddScoped<IChatsService, ChatsService>();
builder.Services.AddScoped<IMessagesService, MessagesService>();
builder.Services.AddScoped<IClubEventsService, ClubEventsService>();
builder.Services.AddSingleton<IEmailService, SendGridEmailService>();
//Security for account sign in
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30); // Lockout for 30 minutes
    options.Lockout.MaxFailedAccessAttempts = 5; // Max 5 access attempts
    options.Lockout.AllowedForNewUsers = true;


})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();


//Authentication and Authorization
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
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

//Authentication
app.UseAuthentication();
app.UseCors();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapHub<ChatHub>("/chatHub");
AppDbInitializer.Seed(app);
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
app.Run();
