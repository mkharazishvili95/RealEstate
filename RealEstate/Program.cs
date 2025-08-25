using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RealEstate.Application.Configuration;
using RealEstate.Application.Feature.Manage.Agency.List;
using RealEstate.Application.Feature.Manage.Apartment.List;
using RealEstate.Application.Feature.Manage.User.List;
using RealEstate.Application.Services;
using RealEstate.Core.Address;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Queries.Apartment;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUserListForManageHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetApartmentListForManageHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAgencyListForManageHandler).Assembly));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IManageService, ManageService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddTransient<IAgencyService, AgencyService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IApartmentQueries, ApartmentQueries>();
builder.Services.AddTransient<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddHttpClient();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.Configure<ImageKitSettings>(
    builder.Configuration.GetSection("ImageKit")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
