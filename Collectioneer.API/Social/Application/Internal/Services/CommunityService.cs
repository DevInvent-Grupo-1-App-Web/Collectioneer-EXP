using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Queries;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;

namespace Collectioneer.API.Social.Application.Internal.Services
{
    public class CommunityService(
        ICommunityRepository communityRepository,
        IPostRepository postRepository,
        IUnitOfWork unitOfWork,
        IRoleService roleService,
        ICollectibleRepository collectibleRepository
    ) : ICommunityService
    {
        public async Task AddUserToCommunity(CommunityJoinCommand command)
        {
            await roleService.CreateNewRole(new CreateRoleCommand(command.UserId, command.CommunityId, (RoleType)3));
            await unitOfWork.CompleteAsync();
        }

        public async Task<CommunityDTO> CreateNewCommunity(CommunityCreateCommand command)
        {
            var newCommunity = new Community(command.Name, command.Description);

            try
            {
                await communityRepository.Add(newCommunity);
                await unitOfWork.CompleteAsync();

                await roleService.CreateNewRole(new CreateRoleCommand(command.UserId, newCommunity.Id, (RoleType)1));
                await unitOfWork.CompleteAsync();

                return new CommunityDTO(newCommunity);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown error creating community.", ex);
            }
        }

        public async Task<ICollection<CommunityDTO>> GetCommunities()
        {
            var communities = await communityRepository.GetAll();
            return communities.Select(c => new CommunityDTO(c)).ToList();
        }

        public async Task<CommunityDTO> GetCommunity(CommunityGetCommand command)
        {
            var community = await communityRepository.GetById(command.Id);
            if (community == null)
            {
                throw new Exception("Community not found.");
            }
            return new CommunityDTO(community);
        }

        public async Task<ICollection<FeedItemDTO>> GetCommunityFeed(CommunityFeedQuery query)
        {
            // Get collectibles from the community
            var collectibles = await collectibleRepository.GetCollectibles(query.CommunityId);

            Console.WriteLine("!!!!!!! Collectibles: " + collectibles.Count);

            // Lambda that returns a string based on the type of the collectible
            var feedItemType = (Collectible c) =>
            {
                if (c.SaleId != null)

                {
                    return FeedItemType.Sale.ToString();
                }
                else if (c.ExchangeId != null)

                {
                    return FeedItemType.Exchange.ToString();
                }
                else if (c.AuctionId != null)

                {
                    return FeedItemType.Auction.ToString();
                }
                else
                {
                    return FeedItemType.Collectible.ToString();
                }
            };

            var feedElements = collectibles.Select(c => new FeedItemDTO(
                c.Id,
                c.MediaElements.Select(m => m.MediaURL).ToList(),
                c.Name,
                c.Description,
                c.CreatedAt,
                c.Owner!.Username,
                c.OwnerId,
                c.CommunityId,
                c.Community!.Name,
                feedItemType(c)
            )).ToList();

            var posts = await postRepository.GetPosts(query.CommunityId);

            var postFeedElements = posts.Select(p => new FeedItemDTO(
                p.Id,
                p.MediaElements.Select(m => m.MediaURL).ToList(),
                p.Title,
                p.Content,
                p.CreatedAt,
                p.Author!.Username,
                p.AuthorId,
                p.CommunityId,
                p.Community!.Name,
                FeedItemType.Post.ToString()
            )).ToList();

            feedElements.AddRange(postFeedElements);

            return feedElements;
        }

        public async Task<ICollection<CommunityDTO>> GetUserCommunities(CommunityFetchByUserQuery query)
        {
            var communities = await roleService.GetUserRoles(query.UserId) ?? throw new Exception("User is has not registered in any communities.");
            var communitiesId = communities.Select(c => c.CommunityId).ToList();
            var userCommunities = new List<Community>();

            foreach (var id in communitiesId)
            {
                var community = await communityRepository.GetById(id);
                if (community != null)
                {
                    userCommunities.Add(community);
                }
            }

            return userCommunities.Select(c => new CommunityDTO(c)).ToList();
        }

        public async Task<ICollection<CommunityDTO>> SearchCommunities(CommunitySearchQuery query)
        {
            var communities = await communityRepository.Search(query.SearchTerm);
            return communities.Select(c => new CommunityDTO(c)).ToList();
        }

		public async Task<ICollection<FeedItemDTO>> SearchInCommunity(CommunitySearchContentQuery query)
		{
			var posts = await postRepository.Search(query.SearchTerm, query.CommunityId);
			var collectibles = await collectibleRepository.Search(query.SearchTerm, query.CommunityId);

			var feedElements = new List<FeedItemDTO>();

			foreach (var post in posts)
			{
				feedElements.Add(new FeedItemDTO(
					post.Id,
					post.MediaElements.Select(m => m.MediaURL).ToList(),
					post.Title,
					post.Content,
					post.CreatedAt,
					post.Author!.Username,
					post.AuthorId,
					post.CommunityId,
					post.Community!.Name,
					FeedItemType.Post.ToString()
				));
			}

			foreach (var collectible in collectibles)
			{
				feedElements.Add(new FeedItemDTO(
					collectible.Id,
					collectible.MediaElements.Select(m => m.MediaURL).ToList(),
					collectible.Name,
					collectible.Description,
					collectible.CreatedAt,
					collectible.Owner!.Username,
					collectible.OwnerId,
					collectible.CommunityId,
					collectible.Community!.Name,
					FeedItemType.Collectible.ToString()
				));
			}

			return feedElements.OrderByDescending(f => f.CreatedAt).ToList();
		}
	}
}
