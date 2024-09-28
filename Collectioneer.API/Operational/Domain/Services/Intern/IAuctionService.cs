using Collectioneer.API.Operational.Application.External;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Operational.Domain.Queries;

namespace Collectioneer.API.Operational.Domain.Services.Intern;

public interface IAuctionService
{
    public Task<AuctionDTO> CreateAuction(AuctionCreationCommand command);
    public Task<AuctionDTO> GetAuction(int id);
    public Task<ICollection<AuctionDTO>> GetAuctions(AuctionBulkRetrieveQuery query);
    public Task<AuctionDTO> GetAuctionFromCollectibleId(AuctionGetByCollectibleIdQuery query);
    public Task<BidDTO> PlaceBid(BidCreationCommand command);
    public Task<ICollection<BidDTO>> GetBids(BidRetrieveQuery query);
    public Task<BidDTO?> CloseAuction(AuctionCloseCommand command);
    public Task AuctioneerConfirmation(AuctionValidationCommand command);
    public Task BidderConfirmation(AuctionValidationCommand command);
}
