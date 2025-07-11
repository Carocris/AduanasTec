using Xunit;
using AduanasTec.Services;
using AduanasTec.Models;
using Moq;
using AduanasTec.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ClienteServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllClientes()
    {
        var mockRepo = new Mock<IClienteRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Cliente>
        {
            new Cliente { Id = 1, Nombre = "Juan", Email = "juan@mail.com", Telefono = "123" }
        });
        var service = new ClienteService(mockRepo.Object);

        var result = await service.GetAllAsync();

        Assert.Single(result);
        Assert.Equal("Juan", result[0].Nombre);
    }
} 