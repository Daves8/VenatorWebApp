using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VenatorWebApp.DAL;
using VenatorWebApp.DAL.Impl;
using VenatorWebApp.Models.Common;
using VenatorWebApp.Services;
using VenatorWebApp.Services.Exceptions;
using VenatorWebApp.Services.Impl;
using VenatorWebApp.Services.Util;
using VenatorWebApp.Services.Util.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => // WithViews();
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});

// Services
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IItemService, ItemService>();
builder.Services.AddSingleton<IContentService, ContentService>();
builder.Services.AddSingleton<IMessageService, MessageService>();

// DAO
builder.Services.AddSingleton<IUserDao, UserDao>();
builder.Services.AddSingleton<IItemDao, ItemDao>();
builder.Services.AddSingleton<IContentDao, ContentDao>();
builder.Services.AddSingleton<IMessageDao, MessageDao>();

// Util
builder.Services.AddSingleton<IFillModelsService, FillModelsService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AuthPolicy.USER_REQUIRE, policy =>
          policy.RequireRole(Role.User.ToString(), Role.Moderator.ToString(), Role.Administrator.ToString()));
    options.AddPolicy(AuthPolicy.MODERATOR_REQUIRE, policy =>
          policy.RequireRole(Role.Moderator.ToString(), Role.Administrator.ToString()));
    options.AddPolicy(AuthPolicy.ADMINISTRATOR_REQUIRE, policy =>
          policy.RequireRole(Role.Administrator.ToString()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
