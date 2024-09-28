using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.Testing
{
    internal class CommunityTest
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            //use In-Memory database for testing (repository layer)
            var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"))
                .AddScoped<Community>()
                .BuildServiceProvider();
            _serviceProvider = serviceProvider;
        }

        [Test]
        public void Test1()
        {
            //arrange
            using var scope = _serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var manager = scopedServices.GetRequiredService<Community>();
            var dbContext = scopedServices.GetRequiredService<AppDbContext>();

            //act
            var createdItem = new Community("test", "desc");

            //assert
            var addedItem = dbContext.Communities.Find(createdItem.Id);
            Assert.IsNotNull(addedItem);
            Assert.AreEqual(createdItem.Name, addedItem.Name);
        }
    }
}
