namespace EShopProject.Controllers;
using EShopProject.Dtos;
using EShopProject.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="productService"></param>
    public ProductsController(
        IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns>List of products DTO</returns>
    [HttpGet("getproducts")]
    public ActionResult GetProducts()
    {
        List<ProductDto> products = _productService.GetProductsDtos();
        return Ok(products);
    }

    /// <summary>
    /// Get product by id
    /// </summary>
    /// <param name="productId"></param>
    /// <returns>A product DTO</returns>
    [HttpGet("{productId}")]
    public ActionResult GetProductById(int productId)
    {
        try
        {
            ProductDto product = _productService.GetProductDtoById(productId);
            return Ok(product);
        }
        catch (InputInvalidException erreur)
        {
            return BadRequest(erreur.Message);
        }
    }

    /// <summary>
    /// Delete product by id
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    [HttpDelete("{productId}")]
    public ActionResult DeleteProductById(int productId)
    {
        try
        {
            _productService.DeleteProductById(productId);
            return Ok();
        }
        catch (InputInvalidException erreur)
        {
            return BadRequest(erreur.Message);
        }
    }

    /// <summary>
    /// Add a product
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    [HttpPost("")]
    public ActionResult AddProduct(AddProductDto product)
    {
        try
        {
            _productService.AddProduct(product);
            return Ok();
        }
        catch (InputInvalidException erreur)
        {
            return BadRequest(erreur.Message);
        }
    }

    /// <summary>
    /// Update a product
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    [HttpPatch("")]
    public ActionResult UpdateProduct(UpdateProductDto product)
    {
        try
        {
            _productService.UpdateProduct(product);
            return Ok();
        }
        catch (InputInvalidException erreur)
        {
            return BadRequest(erreur.Message);
        }
    }
}