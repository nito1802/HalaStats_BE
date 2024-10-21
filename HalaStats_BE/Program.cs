using HalaStats_BE.Database;
using HalaStats_BE.Services;
using HalaStats_Core;
using Microsoft.EntityFrameworkCore;

namespace HalaStats_BE
{
    public class Program
    {
        public static void ConfigureDatabase(IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddDbContext<IHalaStatsDbContext, HalaStatsDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("HalaStatsDatabase")));
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(
            options => options.AddPolicy("AllowCors",
            builder =>
            {
                builder
                    //.WithOrigins("http://localhost:4456") //AllowSpecificOrigins;
                    //.WithOrigins("http://localhost:4456",
                    //"http://localhost:4457") //AllowMultipleOrigins;
                    .AllowAnyOrigin() //AllowAllOrigins;

                    //.WithMethods("GET") //AllowSpecificMethods;
                    //.WithMethods("GET", "PUT") //AllowSpecificMethods;
                    //.WithMethods("GET", "PUT", "POST") //AllowSpecificMethods;
                    .WithMethods("GET", "PUT",
                    "POST", "DELETE") //AllowSpecificMethods;
                                      //.AllowAnyMethod() //AllowAllMethods;

                    //.WithHeaders("Accept", "Content-type", "Origin", "X-Custom-Header");
                    //AllowSpecificHeaders;
                    .AllowAnyHeader(); //AllowAllHeaders;
            })
            );

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddCoreServices();

            builder.Services.AddScoped<IMatchService, MatchService>();

            ConfigureDatabase(builder.Services);

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //app.UseRouting();

            app.UseCors("AllowCors");

            app.MapControllers();

            app.Run();
        }
    }
}