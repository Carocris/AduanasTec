using Xunit;
using AduanasTec.Services;
using AduanasTec.Models;
using Moq;
using AduanasTec.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductoServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllProductos()
    {
        // Arrange
        var mockRepo = new Mock<IProductoRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Prod1", Descripcion = "Desc", Precio = 10, Stock = 5 }
        });
        var service = new ProductoService(mockRepo.Object);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("Prod1", result[0].Nombre);
    }
} 