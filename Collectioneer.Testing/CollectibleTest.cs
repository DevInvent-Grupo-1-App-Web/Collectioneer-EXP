using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Collectioneer.Testing
{
    internal class CollectibleTest
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"))
                .BuildServiceProvider();
            _serviceProvider = serviceProvider;
        }

        [Test]
        public void Test1()
        {
            //arrange
            using var scope = _serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var dbContext = scopedServices.GetRequiredService<AppDbContext>();

            //act
            var createdItem = new Collectible(1, "name", "desc", 1, (float)0.5);
            dbContext.Collectibles.Add(createdItem);
            dbContext.SaveChanges();

            //assert
            var addedItem = dbContext.Collectibles.Find(createdItem.Id);
            Assert.IsNotNull(addedItem);
            Assert.AreEqual(createdItem.Name, addedItem.Name);
        }
    }
}