using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Application.Internal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collectioneer.Testing.Service
{
    internal class UserServiceTest
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            // Configuración del servicio y base de datos en memoria para pruebas
            var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"))
                .AddScoped<UserService>() // Registramos el servicio
                .AddScoped<User>() // Registramos el modelo de usuario
                .BuildServiceProvider();

            _serviceProvider = serviceProvider;
        }

        [Test]
        public void ShouldCreateUserSuccessfully()
        {
            // Arrange
            using var scope = _serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var userService = scopedServices.GetRequiredService<UserService>();
            var dbContext = scopedServices.GetRequiredService<AppDbContext>();

            // Act
            var command = new UserRegisterCommand("test@gmail.com", "test", "test", "test");
            var createdUser = userService.RegisterNewUser(command).Result;

            // Assert
            var addedItem = dbContext.Users.Find(createdUser.Id);
            Assert.IsNotNull(addedItem);
            Assert.AreEqual(createdUser.Name, addedItem.Name);
        }

        [TearDown]
        public void TearDown()
        {
            // Limpiar la base de datos entre pruebas
            var dbContext = _serviceProvider.GetService<AppDbContext>();
            dbContext.Database.EnsureDeleted();
        }
    }
}
