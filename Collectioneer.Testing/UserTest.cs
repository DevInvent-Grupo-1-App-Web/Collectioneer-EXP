using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collectioneer.Testing
{
    internal class UserTest
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            //use In-Memory database for testing (repository layer)
            var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"))
                .AddScoped<User>()
                .BuildServiceProvider();
            _serviceProvider = serviceProvider;
        }

        [Test]
        public void Test1()
        {
            //arrange
            using var scope = _serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var manager = scopedServices.GetRequiredService<User>();
            var dbContext = scopedServices.GetRequiredService<AppDbContext>();

            //act
            var createdUser = new User("test", "test@gmail.com", "test", "test");

            //assert
            var addedItem = dbContext.Users.Find(createdUser.Id);
            Assert.IsNotNull(addedItem);
            Assert.AreEqual(createdUser.Name, addedItem.Name);
        }
    }
}
