using Collectioneer.API.Operational.Application.External;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Queries;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Domain.Services;

namespace Collectioneer.API.Operational.Application.Internal.Services
{
	public class CollectibleService(
			ICollectibleRepository collectibleRepository,
			IReviewService reviewService,
			ILogger<CollectibleService> _logger,
			IUnitOfWork unitOfWork) : ICollectibleService
	{
		private readonly ICollectibleRepository _collectibleRepository = collectibleRepository;
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IReviewService _reviewService = reviewService;
		private readonly ILogger<CollectibleService> _logger = _logger;

		public async Task<CollectibleDTO> RegisterCollectible(CollectibleRegisterCommand command)
		{
			var collectible = new Collectible(
					command.CommunityId,
					command.Name,
					command.Description,
					command.UserId,
					command.Value
			);

			await _collectibleRepository.Add(collectible);
			await _unitOfWork.CompleteAsync();

			var newCollectibleDto = new CollectibleDTO(collectible);
			(newCollectibleDto.Rating, newCollectibleDto.ReviewCount) = await _reviewService.GetCollectibleStats(new CollectibleStatsQuery(collectible.Id));
			_logger.LogInformation("Fetched stats.");
			return new CollectibleDTO(collectible);
		}

		public async Task RegisterAuctionIdInCollectible(CollectibleAuctionIdRegisterCommand command)
		{
			var collectible = await _collectibleRepository.GetById(command.CollectibleId) ?? throw new Exception("Collectible not found.");
			collectible.AuctionId = command.AuctionId;
			await _collectibleRepository.Update(collectible);
			await _unitOfWork.CompleteAsync();
		}

		public async Task<CollectibleDTO> GetCollectible(int id)
		{
			var collectible = await _collectibleRepository.GetById(id) ?? throw new Exception("Collectible not found.");

			var collectibleDto = new CollectibleDTO(collectible);
			(collectibleDto.Rating, collectibleDto.ReviewCount) = await _reviewService.GetCollectibleStats(new CollectibleStatsQuery(collectible.Id));
			
			return collectibleDto;
		}

		public async Task<ICollection<CollectibleDTO>> GetCollectibles(CollectibleBulkRetrieveQuery command)
		{
			var collectibles = await _collectibleRepository.GetCollectibles(command.CommunityId, command.MaxAmount, command.Offset);
			return collectibles.Select(c => new CollectibleDTO(c)).ToList();
		}

		public async Task<ICollection<CollectibleDTO>> SearchCollectibles(CollectibleSearchQuery query)
		{
			var collectibles = await _collectibleRepository.Search(query.SearchTerm, query.CommunityId);
			return collectibles.Select(c => new CollectibleDTO(c)).ToList();
		}
	}
}
