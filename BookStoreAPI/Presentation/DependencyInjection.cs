using System.Security.Claims;
using System.Text;
using BookStoreAPI.Application.DTOs;
using BookStoreAPI.Application.Services;
using BookStoreAPI.Domain.Interfaces;
using BookStoreAPI.Infrastructure.Data;
using BookStoreAPI.Infrastructure.Import;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;

namespace BookStoreAPI.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register custom services
            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IBookImport, BookImportService>();

            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthorService, AuthorService>();

            services.AddScoped<IGenreService, GenreService>();

            services.AddScoped<IReviewService, ReviewService>();


            services.AddDbContext<DBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("BookStoreDB")));

            var seedOptions = configuration
                .GetSection("SeedOptions")
                .Get<SeedOptions>();

            var authOptions = configuration
                .GetSection("AuthOptions")
                .Get<AuthOptions>();

            services.Configure<SeedOptions>(
                configuration.GetSection("SeedOptions"));

            services.Configure<AuthOptions>(
                configuration.GetSection("AuthOptions"));

            services.AddQuartz(q =>
            {

                var jobKey = new JobKey(seedOptions.JobKey);
                q.AddJob<BookImporter>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity(seedOptions.Identity)
                    .WithSimpleSchedule(x => x
                        .WithIntervalInMinutes(seedOptions.IntervalInMinutes.Value)
                        .RepeatForever()));
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        RoleClaimType = ClaimTypes.Role,
                        ValidIssuer = authOptions.Issuer,
                        ValidAudience = authOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SecretKey))
                    };
                });


            return services;
        }
    }
}
