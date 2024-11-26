namespace EShopProject.Tests.Fixtures;
using System.Collections.Generic;
using System.Linq;
using EShopProject.Entities;

class ProductFixture
{
    ProductEntity Sucre;
    ProductEntity CerealesAuChocolat;
    ProductEntity Lait;
    ProductEntity Pain;
    ProductEntity EauGazeuse;
    ProductEntity JusDorange;

    public ProductFixture()
    {
        Sucre = new ProductEntity
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
        CerealesAuChocolat = new ProductEntity
        {
            Id = 2,
            Code = "CRLE",
            Name = "Céréales au chocolat",
            Description = "Les céréales au chocolat sont des céréales de petit-déjeuner à base de maïs soufflé et de chocolat.",
            Image = "cereales-au-chocolat.jpg",
            Category = "Epicerie",
            Price = 2.99F,
            Quantity = 2,
            InternalReference = "CRLEX",
            ShellId = 112,
            Rating = 4,
            InventoryStatus = InventoryStatus.LOWSTOCK,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        Lait = new ProductEntity
        {
            Id = 3,
            Code = "LAIT",
            Name = "Lait",
            Description = "Le lait est un liquide biologique comestible généralement de couleur blanchâtre produit par les glandes mammaires des mammifères femelles.",
            Image = "lait.jpg",
            Category = "Produits frais",
            Price = 1.24F,
            Quantity = 0,
            InternalReference = "LAITX",
            ShellId = 113,
            Rating = 4,
            InventoryStatus = InventoryStatus.OUTOFSTOCK,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        Pain = new ProductEntity
        {
            Id = 4,
            Code = "PAIN",
            Name = "Pain",
            Description = "Le pain est une denrée alimentaire obtenue en faisant cuire de la farine et de l'eau.",
            Image = "pain.jpg",
            Category = "Epicerie",
            Price = 0.95F,
            Quantity = 5,
            InternalReference = "PAINX",
            ShellId = 114,
            Rating = 4,
            InventoryStatus = InventoryStatus.INSTOCK,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        EauGazeuse = new ProductEntity
        {
            Id = 5,
            Code = "EAUF",
            Name = "Eau gazeuse",
            Description = "L'eau gazeuse est une eau minérale naturelle ou une eau de source à laquelle on a ajouté du gaz carbonique.",
            Image = "eau-gazeuse.jpg",
            Category = "Boissons",
            Price = 0.99F,
            Quantity = 2,
            InternalReference = "EAUFX",
            ShellId = 115,
            Rating = 3,
            InventoryStatus = InventoryStatus.LOWSTOCK,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        JusDorange = new ProductEntity
        {
            Id = 6,
            Code = "JUSO",
            Name = "Jus d'orange",
            Description = "Le jus d'orange est une boisson obtenue en pressant des oranges.",
            Image = "jus-d-orange.jpg",
            Category = "Boissons",
            Price = 1.49F,
            Quantity = 0,
            InternalReference = "JUSOX",
            ShellId = 116,
            Rating = 4,
            InventoryStatus = InventoryStatus.OUTOFSTOCK,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public ProductEntity GetSucre()
    {
        return Sucre;
    }

    public ProductEntity GetCerealesAuChocolat()
    {
        return CerealesAuChocolat;
    }

    public ProductEntity GetLait()
    {
        return Lait;
    }

    public ProductEntity GetPain()
    {
        return Pain;
    }

    public ProductEntity GetEauGazeuse()
    {
        return EauGazeuse;
    }

    public ProductEntity GetJusDorange()
    {
        return JusDorange;
    }

    public IQueryable<ProductEntity> GetAllProducts()
    {
        return new List<ProductEntity> { Sucre, CerealesAuChocolat, Lait, Pain, EauGazeuse, JusDorange }.AsQueryable();
    }
}