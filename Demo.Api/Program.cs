using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using Demo.Application;
using Demo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Demo.Infrastructure.Database;

internal class Program
{
    private static void Main(string[] args)
    {       
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddLogging();
        builder.Services.AddControllers();

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();       

        var app = builder.Build();

        //ensure context migration
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider
                .GetRequiredService<DemoContext>();
            dbContext.Database.Migrate();
        }
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

  //      app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}