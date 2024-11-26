namespace EShopProject.Repositories;
using EShopProject.Entities;

public interface IProductRepository : IRepository<ProductEntity>
{
    
}

public class ProductRepository : IProductRepository
{
    private readonly EShopProjectDbContext _eShopProjectDbContext;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="eShopProjectDbContext"></param>
    public ProductRepository(EShopProjectDbContext eShopProjectDbContext)
    {
        _eShopProjectDbContext = eShopProjectDbContext;
    }

    /// <summary>
    /// Get a single product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Product entity</returns>
    public ProductEntity? GetSingle(int id)
    {
        return _eShopProjectDbContext.Products.Where(product => product.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Add a product
    /// </summary>
    /// <param name="product"></param>
    public void Add(ProductEntity product)
    {
        _eShopProjectDbContext.Products.Add(product);
    }

    /// <summary>
    /// Update a product
    /// </summary>
    /// <param name="product"></param>
    /// <returns>Product entity updated</returns>
    public ProductEntity Update(ProductEntity product)
    {
        _eShopProjectDbContext.Products.Update(product);
        return product;
    }

    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns>Products Queryable</returns>
    public IQueryable<ProductEntity> GetAll()
    {
        return _eShopProjectDbContext.Products;
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="product"></param>
    public void Delete(ProductEntity product)
    {
        _eShopProjectDbContext.Products.Remove(product);
    }

    /// <summary>
    /// Save changes
    /// </summary>
    /// <returns></returns>
    public bool Save()
    {
        return _eShopProjectDbContext.SaveChanges() > 0;
    }
}