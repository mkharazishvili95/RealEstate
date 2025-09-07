using Hangfire;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Feature.Manage.Agency.List;
using RealEstate.Application.Feature.Manage.Apartment.List;
using RealEstate.Application.Feature.Manage.User.List;
using RealEstate.Application.Services;
using RealEstate.Core.Address;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Queries.Apartment;
using RealEstate.Infrastructure.Repositories;
using RealEstate_Jobs.Jobs;
using RealEstate_Jobs.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddTransient<IManageService, ManageService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddTransient<IAgencyService, AgencyService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IApartmentQueries, ApartmentQueries>();
builder.Services.AddTransient<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUserListForManageHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetApartmentListForManageHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAgencyListForManageHandler).Assembly));

//Hangfire:
builder.Services.AddScoped<IApartmentJobService, ApartmentJobService>();

builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddHangfireServer();

var app = builder.Build();


app.MapGet("/", () => "Jobs Service is running");

app.UseHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<ApartmentJob>(
    "expire-apartment-job",
    job => job.Execute(),
    "0 */1 * * * *" 
);

app.Run();
