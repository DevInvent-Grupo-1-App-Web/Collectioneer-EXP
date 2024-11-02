using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Application.Internal.Services;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Queries;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collectioneer.Testing.Service
{
    [TestFixture]
    public class CommentServiceTest
    {
        private Mock<ICommentRepository> _commentRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediaElementRepository> _mediaElementRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private CommentService _commentService;

        [SetUp]
        public void Setup()
        {
            _commentRepositoryMock = new Mock<ICommentRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _mediaElementRepositoryMock = new Mock<IMediaElementRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _commentService = new CommentService(
                _commentRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediaElementRepositoryMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Test]
        public async Task GetCommentsForCollectible_ShouldReturnComments()
        {
            // Arrange
            var collectibleId = 1;
            var comments = new List<Comment>
            {
                new Comment(collectibleId, typeof(Collectible), 1, "Content1"),
                new Comment(collectibleId, typeof(Collectible), 2, "Content2")
            };

            _commentRepositoryMock.Setup(r => r.GetCommentsForCollectible(collectibleId)).ReturnsAsync(comments);

            // Act
            var result = await _commentService.GetCommentsForCollectible(collectibleId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(comments.Count));
        }

        [Test]
        public async Task PostComment_ShouldAddCommentSuccessfully()
        {
            // Arrange
            var command = new CommentRegisterCommand(1, "Content") { PostId = 1 };
            var comment = new Comment(command.PostId.Value, typeof(Post), command.AuthorId, command.Content);

            _commentRepositoryMock.Setup(r => r.Add(It.IsAny<Comment>())).ReturnsAsync(comment);
            _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

            // Act
            await _commentService.PostComment(command);

            // Assert
            _commentRepositoryMock.Verify(r => r.Add(It.IsAny<Comment>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }
        [Test]
        public async Task MapCommentToDTO_ShouldReturnCommentDTO()
        {
            // Arrange
            var comment = new Comment(1, typeof(Post), 1, "Content");
            var hashedPassword = new string('a', 64); // Asegúrate de que esta sea una contraseña cifrada válida
            var user = new User("User1", "user1@example.com", "User One", hashedPassword);
            var mediaElement = new MediaElement(1, "MediaName", "MediaURL") { UploaderId = user.Id };

            _userRepositoryMock.Setup(r => r.GetById(comment.AuthorId)).ReturnsAsync(user);
            _mediaElementRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(new List<MediaElement> { mediaElement });

            // Act
            var result = await _commentService.MapCommentToDTO(comment);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Username, Is.EqualTo(user.Username));
            Assert.That(result.ProfileURI, Is.EqualTo(mediaElement.MediaURL));
        }
    }
}