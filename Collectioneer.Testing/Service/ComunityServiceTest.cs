using Collectioneer.API.Social.Application.Internal.Services;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Queries;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;
using Collectioneer.API.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Moq;

namespace Collectioneer.Testing.Service
{
    internal class CommunityServiceTest
    {
        private ServiceProvider _serviceProvider;
        private Mock<ICommunityRepository> _communityRepositoryMock;
        private Mock<IPostRepository> _postRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IRoleService> _roleServiceMock;
        private Mock<ICollectibleRepository> _collectibleRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _communityRepositoryMock = new Mock<ICommunityRepository>();
            _postRepositoryMock = new Mock<IPostRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _roleServiceMock = new Mock<IRoleService>();
            _collectibleRepositoryMock = new Mock<ICollectibleRepository>();

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb")) // Registrar AppDbContext
                .AddScoped(_ => _communityRepositoryMock.Object)
                .AddScoped(_ => _postRepositoryMock.Object)
                .AddScoped(_ => _unitOfWorkMock.Object)
                .AddScoped(_ => _roleServiceMock.Object)
                .AddScoped(_ => _collectibleRepositoryMock.Object)
                .AddScoped<CommunityService>()
                .BuildServiceProvider();

            _serviceProvider = serviceProvider;
        }

        [Test]
        public async Task ShouldAddUserToCommunitySuccessfully()
        {
            // Arrange
            using var scope = _serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var communityService = scopedServices.GetRequiredService<CommunityService>();
            var command = new CommunityJoinCommand(1, 2); // Usar valores int en lugar de Guid

            // Act
            await communityService.AddUserToCommunity(command);

            // Assert
            _roleServiceMock.Verify(r => r.CreateNewRole(It.Is<CreateRoleCommand>(c => c.UserId == command.UserId && c.CommunityId == command.CommunityId && c.RoleType == (RoleType)3)), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Test]
        public async Task ShouldCreateNewCommunitySuccessfully()
        {
            // Arrange
            using var scope = _serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var communityService = scopedServices.GetRequiredService<CommunityService>();
            var command = new CommunityCreateCommand("Test Community", "Test Description", 1); // Usar valores string e int
            var newCommunity = new Community(command.Name, command.Description);
            _communityRepositoryMock.Setup(r => r.Add(It.IsAny<Community>())).ReturnsAsync(newCommunity);

            // Act
            var result = await communityService.CreateNewCommunity(command);

            // Assert
            _communityRepositoryMock.Verify(r => r.Add(It.Is<Community>(c => c.Name == command.Name && c.Description == command.Description)), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Exactly(2));
            _roleServiceMock.Verify(r => r.CreateNewRole(It.Is<CreateRoleCommand>(c => c.UserId == command.UserId && c.CommunityId == newCommunity.Id && c.RoleType == (RoleType)1)), Times.Once);
            Assert.That(result.Name, Is.EqualTo(command.Name));
            Assert.That(result.Description, Is.EqualTo(command.Description));
        }

        [Test]
        public async Task ShouldGetCommunitiesSuccessfully()
        {
            // Arrange
            using var scope = _serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var communityService = scopedServices.GetRequiredService<CommunityService>();
            var communities = new List<Community> { new Community("Test Community", "Description") };
            _communityRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(communities);

            // Act
            var result = await communityService.GetCommunities();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Test Community"));
        }

        [Test]
        public async Task ShouldGetCommunitySuccessfully()
        {
            // Arrange
            using var scope = _serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var communityService = scopedServices.GetRequiredService<CommunityService>();
            var community = new Community("Test Community", "Description");
            _communityRepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(community);

            // Act
            var command = new CommunityGetCommand(1); // Usar valor int
            var result = await communityService.GetCommunity(command);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Test Community"));
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