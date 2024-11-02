using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Application.Internal.Services;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.Aggregates;
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
    public class PostServiceTest
    {
        private Mock<IPostRepository> _postRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private PostService _postService;

        [SetUp]
        public void Setup()
        {
            _postRepositoryMock = new Mock<IPostRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _postService = new PostService(
                _postRepositoryMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Test]
        public async Task AddPost_ShouldAddPostSuccessfully()
        {
            // Arrange
            var command = new AddPostCommand("Title", "Content", 1, 1);
            var post = new Post(command.CommunityId, command.Title, command.Content, command.AuthorId);

            _postRepositoryMock.Setup(r => r.Add(It.IsAny<Post>())).ReturnsAsync(post);
            _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _postService.AddPost(command);

            // Assert
            _postRepositoryMock.Verify(r => r.Add(It.IsAny<Post>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo(command.Title));
        }

        [Test]
        public async Task Search_ShouldReturnPosts()
        {
            // Arrange
            var query = new PostSearchQuery("searchTerm", 1);
            var posts = new List<Post>
            {
                new Post(1, "Title1", "Content1", 1),
                new Post(1, "Title2", "Content2", 1)
            };

            _postRepositoryMock.Setup(r => r.Search(query.SearchTerm, query.CommunityId)).ReturnsAsync(posts);

            // Act
            var result = await _postService.Search(query);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(posts.Count));
        }

        [Test]
        public async Task GetPost_ShouldReturnPost()
        {
            // Arrange
            var post = new Post(1, "Title", "Content", 1);
            _postRepositoryMock.Setup(r => r.GetById(1)).ReturnsAsync(post);

            // Act
            var result = await _postService.GetPost(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo(post.Title));
        }

        [Test]
        public async Task GetPost_ShouldReturnNullWhenPostNotFound()
        {
            // Arrange
            _postRepositoryMock.Setup(r => r.GetById(1)).ReturnsAsync((Post)null);

            // Act
            var result = await _postService.GetPost(1);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}