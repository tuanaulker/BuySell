using BuySell.Business.Application;
using BuySell.Business.Application.Services.Storage.Local;
using BuySell.Business.Infrastructure;
using BuySell.CommonModels.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBusinessPersistenceServices(builder.Configuration);
//builder.Services.AddStorage(StorageType.Azure);
builder.Services.AddStorage<LocalStorage>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //who will use the token  www.site.com
            ValidateIssuer = true, //who is giving the token   www.myapi.com
            ValidateLifetime = true, //checking the token life time
            ValidateIssuerSigningKey = true, //validate that this is my key

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };
    });
builder.Services.AddApplicationServicesBusiness();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseStaticFiles(); // wwwroot için

app.UseAuthentication();
app.UseAuthorization();

InfrastructureBusinessServiceRegistration.Migration(app.Services.CreateScope());

app.MapControllers();

app.Run();
