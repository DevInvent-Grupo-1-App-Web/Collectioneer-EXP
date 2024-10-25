using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
            var password = "test"; // Esta es la contraseña que se va a hashear
            var hashedPassword = HashPassword(password); // Método para hashear la contraseña

            if (hashedPassword.Length != 64) // Longitud esperada para SHA-256 en formato hexadecimal
            {
                throw new Exception("Hashed password is not of expected length.");
            }

            var createdUser = new User("test", "test@gmail.com", "test", hashedPassword);
            dbContext.Users.Add(createdUser);
            dbContext.SaveChanges();

            //assert
            var addedItem = dbContext.Users.Find(createdUser.Id);
            Assert.IsNotNull(addedItem);
            Assert.That(addedItem.Name, Is.EqualTo(createdUser.Name));
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}