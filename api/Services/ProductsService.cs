namespace EShopProject.Services;
using AutoMapper;
using EShopProject.Repositories;
using EShopProject.Entities;
using EShopProject.Dtos;

public interface IProductService
{
    List<ProductEntity> GetProductsEntities();
    List<ProductDto> GetProductsDtos();
    ProductEntity GetProductEntityById(int productId);
    ProductDto GetProductDtoById(int productId);
    void AddProduct(AddProductDto product);
    void DeleteProductById(int productId);
    void UpdateProduct(UpdateProductDto productDto);
}

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="productRepository"></param>
    /// <param name="mapper"></param>
    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    ///  Get all products queryable
    /// </summary>
    /// <returns>Queryable of products entities</returns>
    private IQueryable<ProductEntity> GetProductsQueryable()
    {
        return _productRepository.GetAll();
    }

    /// <summary>
    /// Get all products entities
    /// </summary>
    /// <returns>List of products entities</returns>
    public List<ProductEntity> GetProductsEntities()
    {
        List<ProductEntity> products = GetProductsQueryable().ToList();
        return products;
    }

    /// <summary>
    /// Get all products DTOs
    /// </summary>
    /// <returns>List of products DTOS</returns>
    public List<ProductDto> GetProductsDtos()
    {
        List<ProductDto> products = _mapper.Map<List<ProductDto>>(GetProductsEntities());
        return products;
    }

    /// <summary>
    /// Get a product entity by its id
    /// </summary>
    /// <param name="productId"></param>
    /// <returns>Product entity</returns>
    /// <exception cref="InputInvalidException"></exception>
    public ProductEntity GetProductEntityById(int productId)
    {
        ProductEntity? product = _productRepository.GetSingle(productId)
            ?? throw new InputInvalidException("error_unknown_product");
        return product;
    }

    /// <summary>
    /// Get a product DTO by its id
    /// </summary>
    /// <param name="productId"></param>
    /// <returns>Product DTO</returns>
    public ProductDto GetProductDtoById(int productId)
    {
        ProductDto product = _mapper.Map<ProductDto>(GetProductEntityById(productId));
        return product;
    }

    /// <summary>
    /// Delete a product by its id
    /// </summary>
    /// <param name="productId"></param>
    /// <exception cref="InputInvalidException"></exception>
    public void DeleteProductById(int productId)
    {
        ProductEntity? product = GetProductEntityById(productId);

        _productRepository.Delete(product);

        if (!_productRepository.Save())
        {
            throw new InputInvalidException("error_delete_product_save_fail");
        }
    }

    /// <summary>
    /// Create a new product from productDTO
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="InputInvalidException"></exception>
    public void AddProduct(AddProductDto productDto)
    {
        ProductEntity productEntity = _mapper.Map<ProductEntity>(productDto);

        productEntity.CreatedAt = DateTime.UtcNow;

        _productRepository.Add(productEntity);

        if (!_productRepository.Save())
        {
            throw new InputInvalidException("error_add_product_save_fail");
        }
    }

    /// <summary>
    /// Find the product and update it from productDTO
    /// </summary>
    /// <param name="productDto"></param>
    /// <exception cref="InputInvalidException"></exception>
    public void UpdateProduct(UpdateProductDto productDto)
    {
        ProductEntity productEntity = GetProductEntityById(productDto.Id);

        _mapper.Map(productDto, productEntity);

        _productRepository.Update(productEntity);

        if (!_productRepository.Save())
        {
            throw new InputInvalidException("error_update_product_save_fail");
        }
    }
}