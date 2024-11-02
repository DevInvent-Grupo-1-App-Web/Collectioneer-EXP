using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Collectioneer.API.Operational.Application.Internal.Services;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Queries;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Shared.Domain.Repositories;
using Moq;
using NUnit.Framework;

namespace Collectioneer.API.Tests.Operational.Application.Internal.Services
{
    [TestFixture]
    public class ReviewServiceTests
    {
        private Mock<IReviewRepository> _reviewRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private ReviewService _reviewService;

        [SetUp]
        public void SetUp()
        {
            _reviewRepositoryMock = new Mock<IReviewRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _reviewService = new ReviewService(_reviewRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Test]
        public async Task CreateReview_ShouldAddReviewAndCompleteUnitOfWork()
        {
            // Arrange
            var command = new ReviewCreateCommand(1, 1, "Great collectible!", 5);

            // Act
            var result = await _reviewService.CreateReview(command);

            // Assert
            _reviewRepositoryMock.Verify(r => r.Add(It.IsAny<Review>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
            Assert.AreEqual(command.ReviewerId, result.ReviewerId);
            Assert.AreEqual(command.CollectibleId, result.CollectibleId);
            Assert.AreEqual(command.Content, result.Content);
            Assert.AreEqual(command.Rating, result.Rating);
        }

        [Test]
        public async Task GetCollectibleReviews_ShouldReturnReviews()
        {
            // Arrange
            var query = new CollectibleReviewsQuery(1);
            var reviews = new List<Review> { new Review(1, 1, "Great collectible!", 5) };
            _reviewRepositoryMock.Setup(r => r.GetCollectibleReviews(query.CollectibleId)).ReturnsAsync(reviews);

            // Act
            var result = await _reviewService.GetCollectibleReviews(query);

            // Assert
            Assert.AreEqual(reviews, result);
        }

        [Test]
        public async Task GetCollectibleStats_ShouldReturnCorrectStats()
        {
            // Arrange
            var query = new CollectibleStatsQuery(1);
            var reviews = new List<Review>
            {
                new Review(1, 1, "Great collectible!", 5),
                new Review(2, 1, "Not bad", 3)
            };
            _reviewRepositoryMock.Setup(r => r.GetCollectibleReviews(query.CollectibleId)).ReturnsAsync(reviews);

            // Act
            var (averageRating, reviewCount) = await _reviewService.GetCollectibleStats(query);

            // Assert
            Assert.AreEqual(4.0f, averageRating);
            Assert.AreEqual(2, reviewCount);
        }

        [Test]
        public async Task GetUserReviews_ShouldReturnReviews()
        {
            // Arrange
            var query = new UserReviewsQuery(1);
            var reviews = new List<Review> { new Review(1, 1, "Great collectible!", 5) };
            _reviewRepositoryMock.Setup(r => r.GetUserReviews(query.UserId)).ReturnsAsync(reviews);

            // Act
            var result = await _reviewService.GetUserReviews(query);

            // Assert
            Assert.AreEqual(reviews, result);
        }
    }
}