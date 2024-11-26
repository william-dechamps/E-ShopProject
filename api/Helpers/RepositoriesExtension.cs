namespace EShopProject.Helpers;
using EShopProject.Repositories;

public static class RepositoriesExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}
