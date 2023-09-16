using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Twitter_Clone.Data;
using Twitter_Clone.Interfaces;
using Twitter_Clone.Repositories;
using Twitter_Clone.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Changed from AddControllersWithViews()

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITweetRepository, TweetRepository>();
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<TweetMapper>();

var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
var jwtSettings = jwtSettingsSection.Get<Dictionary<string, string>>();

builder.Services.AddDbContext<TwitterCloneDb>(options =>
    options.UseNpgsql(builder.Configuration["PostgresConnectionString"])); // Added DbContext
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings!["Issuer"],
        ValidAudience = jwtSettings!["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!["Key"]))
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Useful for debugging in development
}

// Add this when you implement JWT authentication
// app.UseAuthentication();
// app.UseAuthorization();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// For APIs, you can use attribute-based routing in controllers
app.MapControllers(); // Added MapControllers

app.Run();