using Hotel.Core.Application;
using Hotel.Infrastructure.Identity;
using Hotel.Infrastructure.Persistence;
using Hotel.Infrastructure.Utils;
using Hotel.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add Dependency injections from Layers
builder.Services.AddPresentationLayer()
                .AddApplicationLayer()
                .AddInfrastructurePersistenceLayer(builder.Configuration.GetConnectionString("DefaultConnection"))
                .AddInfrastructureIdentityLayer(builder.Configuration)
                .AddInfrastructureUtilsLayer(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.UseErrorHandlingMiddleware();

await app.CreateAdminUserDefault();

app.MapControllers();

app.Run();
