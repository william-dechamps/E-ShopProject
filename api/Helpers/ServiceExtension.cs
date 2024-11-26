namespace EShopProject.Helpers;
using EShopProject.Services;

public static class ServicesExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
    }
}
