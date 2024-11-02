using Collectioneer.API.Operational.Application.Internal.Services;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Queries;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Collectioneer.API.Operational.Infrastructure.Repositories;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Collectioneer.Testing.Service
{
    internal class CollectibleServiceTest
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"))
                .AddLogging()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<ICollectibleRepository, CollectibleRepository>()
                .AddScoped<IReviewRepository, ReviewRepository>() // Registro de IReviewRepository
                .AddScoped<IReviewService, ReviewService>()
                .AddScoped<CollectibleService>()
                .BuildServiceProvider();

            _serviceProvider = serviceProvider;
        }

        [Test]
        public async Task ShouldRegisterCollectibleSuccessfully()
        {
            // Arrange
            using var scope = _serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var collectibleService = scopedServices.GetRequiredService<CollectibleService>();
            var dbContext = scopedServices.GetRequiredService<AppDbContext>();

            // Act
            var command = new CollectibleRegisterCommand("name", 1, "Description", 1, 100);
            var createdCollectible = await collectibleService.RegisterCollectible(command);

            // Assert
            var addedItem = await dbContext.Collectibles.FindAsync(createdCollectible.Id);
            Assert.IsNotNull(addedItem);
            Assert.That(addedItem.Name, Is.EqualTo(createdCollectible.Name));
        }

        [Test]
        public async Task ShouldRegisterAuctionIdInCollectibleSuccessfully()
        {
            // Arrange
            using var scope = _serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var collectibleService = scopedServices.GetRequiredService<CollectibleService>();
            var dbContext = scopedServices.GetRequiredService<AppDbContext>();

            var collectible = new Collectible(1, "Test Collectible", "Description", 1, 100);
            dbContext.Collectibles.Add(collectible);
            await dbContext.SaveChangesAsync();

            // Act
            var command = new CollectibleAuctionIdRegisterCommand(collectible.Id, 1);
            await collectibleService.RegisterAuctionIdInCollectible(command);

            // Assert
            var updatedItem = await dbContext.Collectibles.FindAsync(collectible.Id);
            Assert.IsNotNull(updatedItem);
            Assert.That(updatedItem.AuctionId, Is.EqualTo(1));
        }

        [Test]
        public async Task ShouldGetCollectibleSuccessfully()
        {
            // Arrange
            using var scope = _serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var collectibleService = scopedServices.GetRequiredService<CollectibleService>();
            var dbContext = scopedServices.GetRequiredService<AppDbContext>();

            var collectible = new Collectible(1, "Test Collectible", "Description", 1, 100);
            dbContext.Collectibles.Add(collectible);
            await dbContext.SaveChangesAsync();

            // Act
            var retrievedCollectible = await collectibleService.GetCollectible(collectible.Id);

            // Assert
            Assert.IsNotNull(retrievedCollectible);
            Assert.That(retrievedCollectible.Name, Is.EqualTo(collectible.Name));
        }

        [TearDown]
        public void TearDown()
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (dbContext != null)
            {
                dbContext.Database.EnsureDeleted();
            }
        }
    }
}
