using Xunit;
using AduanasTec.Services;
using AduanasTec.Models;
using Moq;
using AduanasTec.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

public class VentaServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllVentas()
    {
        var mockRepo = new Mock<IVentaRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Venta>
        {
            new Venta { Id = 1, ClienteId = 1, Total = 100 }
        });
        var service = new VentaService(mockRepo.Object);

        var result = await service.GetAllAsync();

        Assert.Single(result);
        Assert.Equal(1, result[0].Id);
    }
} 