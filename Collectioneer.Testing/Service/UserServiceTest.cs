using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Application.Internal.Services;
using Collectioneer.API.Shared.Application.External.Services;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Operational.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Collectioneer.API.Operational.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Collectioneer.API.Shared.Application.Exceptions;

namespace Collectioneer.Testing.Service
{
    internal class UserServiceTest
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"JWT_KEY", "test"},
                    {"JWT_ISSUER", "test"},
                    {"JWT_AUDIENCE", "test"},
                    {"CONTENT_SAFETY_KEY", "test"},
                    {"CONTENT_SAFETY_ENDPOINT", "https://test.endpoint"},
                    {"CONTENT_SAFETY_CLIENT_SERVICE_KEY", "test"},
                    {"STORAGE_URL", "https://test.storage.url"},
                    {"STORAGE_ACCOUNT_CONNECTION_STRING", "DefaultEndpointsProtocol=https;AccountName=test;AccountKey=test;EndpointSuffix=core.windows.net"},
                    {"MYSQL_CONNECTION_STRING", "Server=test;Database=test;User=test;Password=test;"},
                    {"COMMUNICATION_SERVICES_CONNECTION_STRING", "Endpoint=https://test.endpoint;AccessKey=test;"}
                }!)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"))
                .AddSingleton<IConfiguration>(configuration)
                .AddLogging()
                .AddScoped<IUnitOfWork, UnitOfWork>() // Registro de IUnitOfWork
                .AddScoped<IUserRepository, UserRepository>() // Registro de IUserRepository
                .AddScoped<ICollectibleRepository, CollectibleRepository>() // Registro de ICollectibleRepository
                .AddScoped<CommunicationService>() // Registro de CommunicationService
                .AddSingleton(provider =>
                {
                    var config = provider.GetRequiredService<IConfiguration>();
                    var logger = provider.GetRequiredService<ILogger<AppKeys>>();
                    return new AppKeys(null, config, logger);
                }) // Registro de AppKeys
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>() // Registro de IHttpContextAccessor
                .AddScoped<UserService>()
                .BuildServiceProvider();

            _serviceProvider = serviceProvider;
        }

        [Test]
        public async Task ShouldCreateUserSuccessfully()
        {
            // Arrange
            using var scope = _serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var userService = scopedServices.GetRequiredService<UserService>();
            var dbContext = scopedServices.GetRequiredService<AppDbContext>();

            // Act
            var command = new UserRegisterCommand("test@gmail.com", "test", "test", "test");
            var createdUser = await userService.RegisterNewUser(command);

            // Assert
            var addedItem = dbContext.Users.Find(createdUser.Id);
            Assert.IsNotNull(addedItem);
            Assert.That(addedItem.Name, Is.EqualTo(createdUser.Name));
        }

        [Test]
        public async Task ShouldThrowExceptionWhenEmailIsNotUnique()
        {
            // Arrange
            using var scope = _serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var userService = scopedServices.GetRequiredService<UserService>();
            var dbContext = scopedServices.GetRequiredService<AppDbContext>();

            var command = new UserRegisterCommand("test@gmail.com", "test", "test", "test");
            await userService.RegisterNewUser(command);

            // Act & Assert
            var duplicateCommand = new UserRegisterCommand("test@gmail.com", "test2", "test2", "test2");
            Assert.ThrowsAsync<DuplicatedCredentialsException>(() => userService.RegisterNewUser(duplicateCommand));
        }

        [TearDown]
        public void TearDown()
        {
            var dbContext = _serviceProvider.GetService<AppDbContext>();
            if (dbContext != null)
            {
                dbContext.Database.EnsureDeleted();
            }
        }
    }
}