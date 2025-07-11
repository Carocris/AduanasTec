using Xunit;
using AduanasTec.Services;
using AduanasTec.Models;
using Moq;
using AduanasTec.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class AuthServiceTests
{
    [Fact]
    public async Task AuthenticateAsync_ReturnsNull_WhenUserNotFound()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb1").Options;
        var context = new AppDbContext(options);
        var config = new ConfigurationBuilder().AddInMemoryCollection().Build();
        var service = new AuthService(context, config);

        var result = await service.AuthenticateAsync("nouser", "pass");
        Assert.Null(result);
    }

    [Fact]
    public async Task AuthenticateAsync_ReturnsUser_WhenCredentialsMatch()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb2").Options;
        var context = new AppDbContext(options);
        context.Usuarios.Add(new Usuario { Username = "admin", PasswordHash = "admin123" });
        context.SaveChanges();
        var config = new ConfigurationBuilder().AddInMemoryCollection().Build();
        var service = new AuthService(context, config);

        var result = await service.AuthenticateAsync("admin", "admin123");
        Assert.NotNull(result);
        Assert.Equal("admin", result.Username);
    }
} 