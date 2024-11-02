using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Collectioneer.API.Social.Application.Internal.Services;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Shared.Domain.Repositories;
using Moq;
using NUnit.Framework;

namespace Collectioneer.API.Tests.Social.Application.Internal.Services
{
    [TestFixture]
    public class RoleServiceTests
    {
        private Mock<IRoleRepository> _roleRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private RoleService _roleService;

        [SetUp]
        public void SetUp()
        {
            _roleRepositoryMock = new Mock<IRoleRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _roleService = new RoleService(_roleRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Test]
        public async Task CreateNewRole_ShouldAddRoleAndCompleteUnitOfWork()
        {
            // Arrange
            var command = new CreateRoleCommand(1, 1, RoleType.User);

            // Act
            await _roleService.CreateNewRole(command);

            // Assert
            _roleRepositoryMock.Verify(r => r.Add(It.IsAny<Role>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Test]
        public void CreateNewRole_ShouldThrowException_WhenRepositoryFails()
        {
            // Arrange
            var command = new CreateRoleCommand(1, 1, RoleType.User);
            _roleRepositoryMock.Setup(r => r.Add(It.IsAny<Role>())).ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(() => _roleService.CreateNewRole(command));
            Assert.That(ex.Message, Is.EqualTo("Unknown error creating role."));
        }

        [Test]
        public async Task GetUserRoles_ShouldReturnRoles()
        {
            // Arrange
            var userId = 1;
            var roles = new List<Role> { new Role(userId, 1, (int)RoleType.User) };
            _roleRepositoryMock.Setup(r => r.GetRolesByUserId(userId)).ReturnsAsync(roles);

            // Act
            var result = await _roleService.GetUserRoles(userId);

            // Assert
            Assert.AreEqual(roles, result);
        }
    }
}