using AngularCBFBackEND;
using AngularCBFBackEND.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AngularCBFBackEND.Config;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


//conexao banco

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Curso")));

builder.Services.AddDbContext<AppIdentityDbContext> (options =>
    options.UseSqlServer(Configuration.GetConnectionString("Default")));

// Add services to the container.



builder.Services.AddCors(options =>
            {
                options.AddPolicy("Devlopment",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
builder.Services.AddMvc();
builder.Services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIdentityServer();

app.UseStaticFiles();

app.UseMvcWithDefaultRoute();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
