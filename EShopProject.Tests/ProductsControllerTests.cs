namespace EShopProject.Tests;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Moq;
using EShopProject.Dtos;
using EShopProject.Repositories;
using EShopProject.Entities;
using EShopProject.Tests.Fixtures;
using System.Linq;
using System.Net;
using System.Text;

public class ProductControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly HttpClient _client;

    public ProductControllerTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
        _client = webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task TestGetAllProducts()
    {
        var productRepositoryMock = new Mock<IProductRepository>();
        List<ProductDto> attendue =
        [
            new() { Id = 1, Code = "SCRE", Name = "Sucre en poudre", Description = "Utilisé en pâtisserie, en confiserie, pour la confection de desserts, de glaces, de caramels, de confitures ... C'est un sucre utilisable dans toutes les situations y compris en décoration pour les cocktails. Un produit indispensable à posséder absolument dans sa cuisine.", Image = "sucre-en-poudre.jpg", Category = "Epicerie", Price = 2.19F, Quantity = 8, InternalReference = "SCREX", ShellId = 111, Rating = 4, InventoryStatus = InventoryStatus.INSTOCK.ToString() },
            new() { Id = 2, Code = "CRLE", Name = "Céréales au chocolat", Description = "Les céréales au chocolat sont des céréales de petit-déjeuner à base de maïs soufflé et de chocolat.", Image = "cereales-au-chocolat.jpg", Category = "Epicerie", Price = 2.99F, Quantity = 2, InternalReference = "CRLEX", ShellId = 112, Rating = 4, InventoryStatus = InventoryStatus.LOWSTOCK.ToString() },
            new() { Id = 3, Code = "LAIT", Name = "Lait", Description = "Le lait est un liquide biologique comestible généralement de couleur blanchâtre produit par les glandes mammaires des mammifères femelles.", Image = "lait.jpg", Category = "Produits frais", Price = 1.24F, Quantity = 0, InternalReference = "LAITX", ShellId = 113, Rating = 4, InventoryStatus = InventoryStatus.OUTOFSTOCK.ToString() },
            new() { Id = 4, Code = "PAIN", Name = "Pain", Description = "Le pain est une denrée alimentaire obtenue en faisant cuire de la farine et de l'eau.", Image = "pain.jpg", Category = "Epicerie", Price = 0.95F, Quantity = 5, InternalReference = "PAINX", ShellId = 114, Rating = 4, InventoryStatus = InventoryStatus.INSTOCK.ToString() },
            new() { Id = 5, Code = "EAUF", Name = "Eau gazeuse", Description = "L'eau gazeuse est une eau minérale naturelle ou une eau de source à laquelle on a ajouté du gaz carbonique.", Image = "eau-gazeuse.jpg", Category = "Boissons", Price = 0.99F, Quantity = 2, InternalReference = "EAUFX", ShellId = 115, Rating = 3, InventoryStatus = InventoryStatus.LOWSTOCK.ToString() },
            new() { Id = 6, Code = "JUSO", Name = "Jus d'orange", Description = "Le jus d'orange est une boisson obtenue en pressant des oranges.", Image = "jus-d-orange.jpg", Category = "Boissons", Price = 1.49F, Quantity = 0, InternalReference = "JUSOX", ShellId = 116, Rating = 4, InventoryStatus = InventoryStatus.OUTOFSTOCK.ToString() },
        ];

        IQueryable<ProductEntity> source = new ProductFixture().GetAllProducts();

        productRepositoryMock.Setup(productRepository => productRepository.GetAll()).Returns(source);
        var webApplicationFactoryWithServiceMock = _webApplicationFactory.WithWebHostBuilder(
            builder => builder.ConfigureTestServices(
                services => services.AddScoped(serviceProvider => productRepositoryMock.Object)
            )
        );

        var clientWithMockService = webApplicationFactoryWithServiceMock.CreateClient();
        var reponse = await clientWithMockService.GetAsync("api/products/getProducts");
        var resultat = await reponse.Content.ReadAsStringAsync();

        List<ProductDto>? productList = JsonConvert.DeserializeObject<List<ProductDto>>(resultat);

        productList.Should().BeEquivalentTo(attendue);
    }

    [Fact]
    public async Task TestGetProductById()
    {
        var productRepositoryMock = new Mock<IProductRepository>();
        ProductDto attendue = new() { Id = 2, Code = "CRLE", Name = "Céréales au chocolat", Description = "Les céréales au chocolat sont des céréales de petit-déjeuner à base de maïs soufflé et de chocolat.", Image = "cereales-au-chocolat.jpg", Category = "Epicerie", Price = 2.99F, Quantity = 2, InternalReference = "CRLEX", ShellId = 112, Rating = 4, InventoryStatus = InventoryStatus.LOWSTOCK.ToString() };
        ProductEntity source = new() { Id = 2, Code = "CRLE", Name = "Céréales au chocolat", Description = "Les céréales au chocolat sont des céréales de petit-déjeuner à base de maïs soufflé et de chocolat.", Image = "cereales-au-chocolat.jpg", Category = "Epicerie", Price = 2.99F, Quantity = 2, InternalReference = "CRLEX", ShellId = 112, Rating = 4, InventoryStatus = InventoryStatus.LOWSTOCK, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };

        productRepositoryMock.Setup(productRepository => productRepository.GetSingle(2)).Returns(source);
        var webApplicationFactoryWithServiceMock = _webApplicationFactory.WithWebHostBuilder(
            builder => builder.ConfigureTestServices(
                services => services.AddScoped(serviceProvider => productRepositoryMock.Object)
            )
        );

        var clientWithMockService = webApplicationFactoryWithServiceMock.CreateClient();
        var reponse = await clientWithMockService.GetAsync("api/products/2");
        var resultat = await reponse.Content.ReadAsStringAsync();

        ProductDto? product = JsonConvert.DeserializeObject<ProductDto>(resultat);

        product.Should().BeEquivalentTo(attendue);
        productRepositoryMock.Verify(mock => mock.GetSingle(attendue.Id), Times.Once());
    }

    [Fact]
    public async Task TestGetProductById_ShouldReturn400()
    {
        var productRepositoryMock = new Mock<IProductRepository>();

        productRepositoryMock.Setup(productRepository => productRepository.GetSingle(1)).Returns((ProductEntity?)null);
        var webApplicationFactoryWithServiceMock = _webApplicationFactory.WithWebHostBuilder(
            builder => builder.ConfigureTestServices(
                services => services.AddScoped(serviceProvider => productRepositoryMock.Object)
            )
        );

        var clientWithMockService = webApplicationFactoryWithServiceMock.CreateClient();
        var reponse = await clientWithMockService.GetAsync("api/products/1");

        reponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task TestAddProduct()
    {
        var productRepositoryMock = new Mock<IProductRepository>();
        AddProductDto source = new()
        {
            Code = "SCRE",
            Name = "Sucre en poudre",
            Description = "Utilisé en pâtisserie, en confiserie, pour la confection de desserts, de glaces, de caramels, de confitures ... C'est un sucre utilisable dans toutes les situations y compris en décoration pour les cocktails. Un produit indispensable à posséder absolument dans sa cuisine.",
            Image = "sucre-en-poudre.jpg",
            Category = "Epicerie",
            Price = 2.19F,
            Quantity = 8,
            InternalReference = "SCREX",
            ShellId = 111,
            Rating = 4,
            InventoryStatus = InventoryStatus.INSTOCK.ToString(),
        };
        ProductEntity enregistree = new()
        {
            Id = 1,
            Code = "SCRE",
            Name = "Sucre en poudre",
            Description = "Utilisé en pâtisserie, en confiserie, pour la confection de desserts, de glaces, de caramels, de confitures ... C'est un sucre utilisable dans toutes les situations y compris en décoration pour les cocktails. Un produit indispensable à posséder absolument dans sa cuisine.",
            Image = "sucre-en-poudre.jpg",
            Category = "Epicerie",
            Price = 2.19F,
            Quantity = 8,
            InternalReference = "SCREX",
            ShellId = 111,
            Rating = 4,
            InventoryStatus = InventoryStatus.INSTOCK,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        productRepositoryMock.Setup(
            productRepository => productRepository.Add(It.IsAny<ProductEntity>()))
            .Callback<ProductEntity>(product => product.Id = 1)
            .Verifiable();
        productRepositoryMock.Setup(productRepository => productRepository.Save()).Returns(true);

        var webApplicationFactoryAvecServiceMock = _webApplicationFactory.WithWebHostBuilder(
            builder => builder.ConfigureTestServices(
                services => services.AddScoped(serviceProvider => productRepositoryMock.Object)
            )
        );

        var clientWithMockService = webApplicationFactoryAvecServiceMock.CreateClient();
        var requestContent = new StringContent(JsonConvert.SerializeObject(source), Encoding.UTF8, "application/json");
        var reponse = await clientWithMockService.PostAsync("api/products", requestContent);

        reponse.StatusCode.Should().Be(HttpStatusCode.OK);
        productRepositoryMock.Verify(mock => mock.Add(It.Is<ProductEntity>(p =>
            p.Code == enregistree.Code &&
            p.Name == enregistree.Name &&
            p.Description == enregistree.Description &&
            p.Image == enregistree.Image &&
            p.Category == enregistree.Category &&
            p.Price == enregistree.Price &&
            p.Quantity == enregistree.Quantity &&
            p.InternalReference == enregistree.InternalReference &&
            p.ShellId == enregistree.ShellId &&
            p.Rating == enregistree.Rating &&
            p.InventoryStatus == enregistree.InventoryStatus
        )), Times.Once());
        productRepositoryMock.Verify(mock => mock.Save(), Times.Once());
    }

    [Fact]
    public async Task TestAddProduct_ShouldReturn400()
    {
        var productRepositoryMock = new Mock<IProductRepository>();
        AddProductDto? source = null;

        productRepositoryMock.Setup(
            productRepository => productRepository.Add(It.IsAny<ProductEntity>())).Callback<ProductEntity>(product => product.Id = 1
        ).Verifiable();
        var webApplicationFactoryAvecServiceMock = _webApplicationFactory.WithWebHostBuilder(
                builder => builder.ConfigureTestServices(
                    services => services.AddScoped(serviceProvider => productRepositoryMock.Object)
                    )
                );
        var clientWithMockService = webApplicationFactoryAvecServiceMock.CreateClient();
        var requestContent = new StringContent(JsonConvert.SerializeObject(source), Encoding.UTF8, "application/json");
        var reponse = await clientWithMockService.PostAsync("api/products", requestContent);

        reponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        productRepositoryMock.Verify(mock => mock.Save(), Times.Never());
    }

    [Fact]
    public async Task TestDeleteProduct()
    {
        var productRepositoryMock = new Mock<IProductRepository>();
        ProductEntity source = new()
        {
            Id = 1,
            Code = "SCRE",
            Name = "Sucre en poudre",
            Description = "Utilisé en pâtisserie, en confiserie, pour la confection de desserts, de glaces, de caramels, de confitures ... C'est un sucre utilisable dans toutes les situations y compris en décoration pour les cocktails. Un produit indispensable à posséder absolument dans sa cuisine.",
            Image = "sucre-en-poudre.jpg",
            Category = "Epicerie",
            Price = 2.19F,
            Quantity = 8,
            InternalReference = "SCREX",
            ShellId = 111,
            Rating = 4,
            InventoryStatus = InventoryStatus.INSTOCK,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        productRepositoryMock.Setup(productRepository => productRepository.GetSingle(1)).Returns(source);
        productRepositoryMock.Setup(productRepository => productRepository.Delete(It.IsAny<ProductEntity>()));
        productRepositoryMock.Setup(productRepository => productRepository.Save()).Returns(true);

        var webApplicationFactoryWithServiceMock = _webApplicationFactory.WithWebHostBuilder(
            builder => builder.ConfigureTestServices(
                services => services.AddScoped(serviceProvider => productRepositoryMock.Object)
            )
        );

        var clientWithMockService = webApplicationFactoryWithServiceMock.CreateClient();
        var reponse = await clientWithMockService.DeleteAsync("api/products/1");

        reponse.StatusCode.Should().Be(HttpStatusCode.OK);
        productRepositoryMock.Verify(mock => mock.GetSingle(1), Times.Once());
        productRepositoryMock.Verify(mock => mock.Delete(source), Times.Once());
        productRepositoryMock.Verify(mock => mock.Save(), Times.Once());
    }

    [Fact]
    public async Task TestDeleteProduct_ShouldReturn400()
    {
        var productRepositoryMock = new Mock<IProductRepository>();

        productRepositoryMock.Setup(productRepository => productRepository.GetSingle(1)).Returns((ProductEntity?)null);
        var webApplicationFactoryAvecServiceMock = _webApplicationFactory.WithWebHostBuilder(
                builder => builder.ConfigureTestServices(
                    services => services.AddScoped(serviceProvider => productRepositoryMock.Object)
                    )
                );
        var clientWithMockService = webApplicationFactoryAvecServiceMock.CreateClient();
        var reponse = await clientWithMockService.DeleteAsync("api/products/1");

        reponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        productRepositoryMock.Verify(mock => mock.Update(It.IsAny<ProductEntity>()), Times.Never());
        productRepositoryMock.Verify(mock => mock.Save(), Times.Never());
    }
}