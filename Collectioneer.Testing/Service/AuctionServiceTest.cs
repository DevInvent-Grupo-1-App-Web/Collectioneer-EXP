using Collectioneer.API.Operational.Application.Internal.Services;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Operational.Domain.Models.Exceptions;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Operational.Domain.Queries;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Collectioneer.API.Shared.Application.Exceptions;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collectioneer.Testing.Service
{
    [TestFixture]
    public class AuctionServiceTest
    {
        private Mock<IAuctionRepository> _auctionRepositoryMock;
        private Mock<ICollectibleService> _collectibleServiceMock;
        private Mock<IBidRepository> _bidRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private AuctionService _auctionService;

        [SetUp]
        public void Setup()
        {
            _auctionRepositoryMock = new Mock<IAuctionRepository>();
            _collectibleServiceMock = new Mock<ICollectibleService>();
            _bidRepositoryMock = new Mock<IBidRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _auctionService = new AuctionService(
                _auctionRepositoryMock.Object,
                _collectibleServiceMock.Object,
                _bidRepositoryMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Test]
        public async Task CreateAuction_ShouldCreateAuctionSuccessfully()
        {
            // Arrange
            var command = new AuctionCreationCommand(1, 1, 1, 100, DateTime.UtcNow.AddDays(1));
            var auction = new Auction(command.CommunityId, command.AuctioneerId, command.CollectibleId, command.StartingPrice, command.Deadline);

            _auctionRepositoryMock.Setup(r => r.Add(It.IsAny<Auction>())).Returns(Task.FromResult(auction));
            _collectibleServiceMock.Setup(s => s.RegisterAuctionIdInCollectible(It.IsAny<CollectibleAuctionIdRegisterCommand>())).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _auctionService.CreateAuction(command);

            // Assert
            _auctionRepositoryMock.Verify(r => r.Add(It.IsAny<Auction>()), Times.Once);
            _collectibleServiceMock.Verify(s => s.RegisterAuctionIdInCollectible(It.IsAny<CollectibleAuctionIdRegisterCommand>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Exactly(2));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.CommunityId, Is.EqualTo(command.CommunityId));
        }

        [Test]
        public async Task PlaceBid_ShouldPlaceBidSuccessfully()
        {
            // Arrange
            var command = new BidCreationCommand(1, 1, 200);
            var auction = new Auction(1, 1, 1, 100, DateTime.UtcNow.AddDays(1));
            var bid = new Bid(command.AuctionId, command.BidderId, command.Amount);

            _auctionRepositoryMock.Setup(r => r.GetById(command.AuctionId)).ReturnsAsync(auction);
            _bidRepositoryMock.Setup(r => r.Add(It.IsAny<Bid>())).Returns(Task.FromResult(bid));
            _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _auctionService.PlaceBid(command);

            // Assert
            _bidRepositoryMock.Verify(r => r.Add(It.IsAny<Bid>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.AuctionId, Is.EqualTo(command.AuctionId));
        }

        [Test]
        public async Task GetAuction_ShouldReturnAuction()
        {
            // Arrange
            var auction = new Auction(1, 1, 1, 100, DateTime.UtcNow.AddDays(1));
            _auctionRepositoryMock.Setup(r => r.GetById(1)).ReturnsAsync(auction);

            // Act
            var result = await _auctionService.GetAuction(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.CommunityId, Is.EqualTo(auction.CommunityId));
        }

        [Test]
        public void GetAuction_ShouldThrowEntityNotFoundException()
        {
            // Arrange
            _auctionRepositoryMock.Setup(r => r.GetById(1)).ReturnsAsync(default(Auction));

            // Act & Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await _auctionService.GetAuction(1));
        }

        // Agrega más pruebas para los otros métodos de AuctionService según sea necesario.
    }
}