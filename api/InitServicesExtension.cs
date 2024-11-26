using EShopProject.Helpers;
using EShopProject.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace EShopProject
{
    public static class InitServicesExtension
    {
        public static void InitServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EShopProjectDbContext>(options => options.UseNpgsql(connectionString));

            services.AddRepositories();
            services.AddServices();

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EShopProject",
                });
            });

            services.AddAutoMapper(typeof(EShopProjectMappings));
            services.AddCustomCors("AllowAllOrigins");
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
        }

    }
}
