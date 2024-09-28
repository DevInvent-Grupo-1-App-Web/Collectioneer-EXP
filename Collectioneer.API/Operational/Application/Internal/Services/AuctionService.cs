using Collectioneer.API.Operational.Application.External;
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
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Operational.Application.Internal.Services;

public class AuctionService(
		IAuctionRepository auctionRepository,
		ICollectibleService collectibleService,
		IBidRepository bidRepository,
		IUnitOfWork unitOfWork) : IAuctionService
{
	private readonly IAuctionRepository _auctionRepository = auctionRepository;
	private readonly ICollectibleService _collectibleService = collectibleService;
	private readonly IBidRepository _bidRepository = bidRepository;
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	public async Task<AuctionDTO> CreateAuction(AuctionCreationCommand command)
	{
		var auction = new Auction(
			command.CommunityId,
			command.AuctioneerId,
			command.CollectibleId,
			command.StartingPrice,
			DateTime.SpecifyKind(command.Deadline, DateTimeKind.Utc)
			);
		try
		{
			await _auctionRepository.Add(auction);
			await _unitOfWork.CompleteAsync();

			await _collectibleService.RegisterAuctionIdInCollectible(new CollectibleAuctionIdRegisterCommand(command.CollectibleId, auction.Id));
			await _unitOfWork.CompleteAsync();
		}
		catch (DbUpdateException ex)
		{
			throw new ExposableException("Invalid entity referenced in request.", 400, ex);
		}
		catch (Exception ex)
		{
			if (ex is ExposableException)
			{
				throw;
			}
			else
			{
				throw new ExposableException("An error occurred while creating the auction.", 500, ex);
			}
		}

		return new AuctionDTO(auction);
	}

	public async Task<BidDTO> PlaceBid(BidCreationCommand command)
	{
		var bid = new Bid(command.AuctionId, command.BidderId, command.Amount);
		var auction = await _auctionRepository.GetById(command.AuctionId) ?? throw new EntityNotFoundException("Auction not found.");

		var lastBid = auction.Bids.LastOrDefault();

		if (lastBid != null && bid.Amount <= lastBid.Amount)
		{
			throw new AuctionModelException("Bid amount must be greater than the last bid amount");
		}

		await _bidRepository.Add(bid);
		await _unitOfWork.CompleteAsync();
		return new BidDTO(bid);
	}

	public async Task<ICollection<BidDTO>> GetBids(BidRetrieveQuery query)
	{
		var bids = await _bidRepository.GetBidsByAuctionId(query.AuctionId);
		return bids.Select(b => new BidDTO(b)).ToList();
	}

	public async Task<BidDTO?> CloseAuction(AuctionCloseCommand command)
	{
		var auction = await _auctionRepository.GetById(command.AuctionId) ?? throw new EntityNotFoundException("Auction not found");
		var bids = await GetBids(new BidRetrieveQuery(command.AuctionId));
		bids = bids.OrderByDescending(b => b.Amount).ToList();
		auction.Close();
		await _auctionRepository.Update(auction);
		await _unitOfWork.CompleteAsync();
		var winningBid = bids.FirstOrDefault();
		return winningBid;
	}

	public async Task AuctioneerConfirmation(AuctionValidationCommand command)
	{
		var auction = await _auctionRepository.GetById(command.AuctionId) ?? throw new EntityNotFoundException("Auction not found");
		auction.CollectAuctioneer();
		await _auctionRepository.Update(auction);
		await _unitOfWork.CompleteAsync();
	}

	public async Task BidderConfirmation(AuctionValidationCommand command)
	{
		var auction = await _auctionRepository.GetById(command.AuctionId) ?? throw new EntityNotFoundException("Auction not found");
		auction.CollectBidder();
		await _auctionRepository.Update(auction);
		await _unitOfWork.CompleteAsync();
	}

	public async Task<AuctionDTO> GetAuction(int id)
	{
		var auction = await _auctionRepository.GetById(id);
		if (auction == null)
		{
			throw new EntityNotFoundException("Auction not found");
		}
		return new AuctionDTO(auction);
	}
	public async Task<ICollection<AuctionDTO>> GetAuctions(AuctionBulkRetrieveQuery query)
	{
		var auctions = await _auctionRepository.GetAuctions(query.CommunityId, query.MaxAmount, query.Offset);
		return auctions.Select(c => new AuctionDTO(c)).ToList();
	}

	public async Task<AuctionDTO> GetAuctionFromCollectibleId(AuctionGetByCollectibleIdQuery query)
	{
		var auction = await _auctionRepository.GetAuctionByCollectibleId(query.CollectibleId);
		if (auction == null)
		{
			throw new EntityNotFoundException($"Couldn't found an auction for collectible with id {query.CollectibleId}");
		}
		return new AuctionDTO(auction);
	}
}
