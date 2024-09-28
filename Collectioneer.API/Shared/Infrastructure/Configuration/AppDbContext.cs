using Microsoft.EntityFrameworkCore;
using Collectioneer.API.Shared.Infrastructure.Configuration.Extensions;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Domain.Models.Entities;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Shared.Infrastructure.Configuration
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public DbSet<User> Users { get; set; }
		public DbSet<MediaElement> MediaElements { get; set; }
		public DbSet<Auction> Auctions { get; set; }
		public DbSet<Exchange> Exchanges { get; set; }
		public DbSet<Sale> Sales { get; set; }
		public DbSet<Collectible> Collectibles { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Bid> Bids { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Community> Communities { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Filter> Filters { get; set; }
		public DbSet<FilterType> FilterTypes { get; set; }
		public DbSet<PostTag> PostTags { get; set; }
		public DbSet<Reaction> Reactions { get; set; }
		public DbSet<Tag> Tags { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			AuctionModelBuilder(modelBuilder);
			ExchangeModelBuilder(modelBuilder);
			SaleModelBuilder(modelBuilder);
			CollectibleModelBuilder(modelBuilder);
			ReviewModelBuilder(modelBuilder);
			BidModelBuilder(modelBuilder);
			RoleModelBuilder(modelBuilder);
			UserModelBuilder(modelBuilder);
			MediaElementModelBuilder(modelBuilder);
			CommunityModelBuilder(modelBuilder);
			PostModelBuilder(modelBuilder);
			CommentModelBuilder(modelBuilder);
			FilterModelBuilder(modelBuilder);
			FilterTypeModelBuilder(modelBuilder);
			PostTagModelBuilder(modelBuilder);
			ReactionModelBuilder(modelBuilder);
			TagModelBuilder(modelBuilder);

			RelationshipBuilder(modelBuilder);

			modelBuilder.UseSnakeCaseNamingConvention();
		}

		private static void AuctionModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Auction>()
					.ToTable("Auctions");

			modelBuilder.Entity<Auction>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Auction>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Auction>()
					.Property(x => x.AuctioneerId)
					.IsRequired();

			modelBuilder.Entity<Auction>()
					.Property(x => x.CollectibleId)
					.IsRequired();

			modelBuilder.Entity<Auction>()
					.Property(x => x.StartingPrice)
					.IsRequired();

			modelBuilder.Entity<Auction>()
					.Property(x => x.Deadline)
					.IsRequired();

			modelBuilder.Entity<Auction>()
					.Property(x => x.IsOpen)
					.IsRequired()
					.HasDefaultValue(true);

			modelBuilder.Entity<Auction>()
					.Property(x => x.AuctioneerHasCollected)
					.IsRequired()
					.HasDefaultValue(false);

			modelBuilder.Entity<Auction>()
					.Property(x => x.BidderHasCollected)
					.IsRequired()
					.HasDefaultValue(false);

			modelBuilder.Entity<Auction>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Auction>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedNever();
		}
		private static void ExchangeModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Exchange>()
					.ToTable("Exchanges");

			modelBuilder.Entity<Exchange>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Exchange>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Exchange>()
					.Property(x => x.CommunityId)
					.IsRequired();

			modelBuilder.Entity<Exchange>()
					.Property(x => x.ExchangerId)
					.IsRequired();

			modelBuilder.Entity<Exchange>()
					.Property(x => x.CollectibleId)
					.IsRequired();

			modelBuilder.Entity<Exchange>()
					.Property(x => x.Price)
					.IsRequired();

			modelBuilder.Entity<Exchange>()
					.Property(x => x.IsOpen)
					.IsRequired()
					.HasDefaultValue(true);

			modelBuilder.Entity<Exchange>()
					.Property(x => x.ExchangerHasConfirmed)
					.IsRequired()
					.HasDefaultValue(false);

			modelBuilder.Entity<Exchange>()
					.Property(x => x.AcceptedExchangeId);

			modelBuilder.Entity<Exchange>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Exchange>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedNever();
		}
		private static void SaleModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Sale>()
					.ToTable("Sales");

			modelBuilder.Entity<Sale>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Sale>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Sale>()
					.Property(x => x.CommunityId)
					.IsRequired();

			modelBuilder.Entity<Sale>()
					.Property(x => x.VendorId)
					.IsRequired();

			modelBuilder.Entity<Sale>()
					.Property(x => x.CollectibleId)
					.IsRequired();

			modelBuilder.Entity<Sale>()
					.Property(x => x.BuyerId);

			modelBuilder.Entity<Sale>()
					.Property(x => x.Price)
					.IsRequired();

			modelBuilder.Entity<Sale>()
					.Property(x => x.IsOpen)
					.IsRequired()
					.HasDefaultValue(true);

			modelBuilder.Entity<Sale>()
					.Property(x => x.VendorHasCollected)
					.IsRequired()
					.HasDefaultValue(false);

			modelBuilder.Entity<Sale>()
					.Property(x => x.BuyerHasCollected)
					.IsRequired()
					.HasDefaultValue(false);

			modelBuilder.Entity<Sale>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Sale>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedNever();
		}
		private static void CollectibleModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Collectible>()
					.ToTable("Collectibles");

			modelBuilder.Entity<Collectible>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Collectible>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Collectible>()
					.Property(x => x.CommunityId)
					.IsRequired();

			modelBuilder.Entity<Collectible>()
					.Property(x => x.Name)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<Collectible>()
					.Property(x => x.OwnerId)
					.IsRequired();

			modelBuilder.Entity<Collectible>()
					.Property(x => x.Value);

			modelBuilder.Entity<Collectible>()
					.Property(x => x.Description)
					.HasMaxLength(2048);

			modelBuilder.Entity<Collectible>()
					.Property(x => x.AuctionId);

			modelBuilder.Entity<Collectible>()
					.Property(x => x.SaleId);

			modelBuilder.Entity<Collectible>()
					.Property(x => x.ExchangeId);

			modelBuilder.Entity<Collectible>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Collectible>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedNever();
		}
		private static void ReviewModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Review>()
								.ToTable("Reviews");

			modelBuilder.Entity<Review>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Review>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Review>()
					.Property(x => x.ReviewerId)
					.IsRequired();

			modelBuilder.Entity<Review>()
					.Property(x => x.CollectibleId)
					.IsRequired();

			modelBuilder.Entity<Review>()
					.Property(x => x.Content);

			modelBuilder.Entity<Review>()
					.Property(x => x.Rating)
					.IsRequired();

			modelBuilder.Entity<Review>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Review>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedNever();
		}
		private static void BidModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Bid>()
					.ToTable("Bids");

			modelBuilder.Entity<Bid>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Bid>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Bid>()
					.Property(x => x.AuctionId)
					.IsRequired();

			modelBuilder.Entity<Bid>()
					.Property(x => x.BidderId)
					.IsRequired();

			modelBuilder.Entity<Bid>()
					.Property(x => x.Amount)
					.IsRequired();

			modelBuilder.Entity<Bid>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Bid>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedNever();
		}
		private static void RoleModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Role>()
					.ToTable("Roles");

			modelBuilder.Entity<Role>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Role>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Role>()
					.Property(x => x.UserId)
					.IsRequired();

			modelBuilder.Entity<Role>()
					.Property(x => x.CommunityId)
					.IsRequired();

			modelBuilder.Entity<Role>()
					.Property(x => x.RoleTypeId)
					.IsRequired();

			modelBuilder.Entity<Role>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Role>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedNever();
		}
		private static void UserModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
					.ToTable("Users");

			modelBuilder.Entity<User>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<User>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<User>()
					.Property(x => x.Username)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<User>()
					.Property(x => x.Email)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<User>()
					.Property(x => x.Name)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<User>()
					.Property(x => x.Password)
					.IsRequired()
					.HasMaxLength(64);
		}
		private static void MediaElementModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<MediaElement>()
					.ToTable("MediaElements");

			modelBuilder.Entity<MediaElement>()
			.HasKey(x => x.Id);

			modelBuilder.Entity<MediaElement>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<MediaElement>()
					.Property(x => x.UploaderId)
					.IsRequired();

			modelBuilder.Entity<MediaElement>()
					.Property(x => x.MediaName)
					.IsRequired()
					.HasMaxLength(256);

			modelBuilder.Entity<MediaElement>()
					.Property(x => x.MediaURL)
					.IsRequired()
					.HasMaxLength(256);

			modelBuilder.Entity<MediaElement>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<MediaElement>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedNever();

			modelBuilder.Entity<MediaElement>()
					.Property(x => x.CollectibleId);

			modelBuilder.Entity<MediaElement>()
					.Property(x => x.PostId);

			modelBuilder.Entity<MediaElement>()
					.Property(x => x.ProfileId);
		}
		private static void CommunityModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Community>()
					.ToTable("Communities");

			modelBuilder.Entity<Community>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Community>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Community>()
					.Property(x => x.Name)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<Community>()
					.Property(x => x.Description)
					.HasMaxLength(500);
		}
		private static void PostModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Post>()
					.ToTable("Posts");

			modelBuilder.Entity<Post>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Post>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Post>()
				.Property(x => x.AuthorId)
				.IsRequired();

			modelBuilder.Entity<Post>()
					.Property(x => x.CommunityId)
					.IsRequired();

			modelBuilder.Entity<Post>()
					.Property(x => x.Title)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<Post>()
					.Property(x => x.IsHidden)
					.IsRequired()
					.HasDefaultValue(false);

			modelBuilder.Entity<Post>()
					.Property(x => x.IsArchived)
					.IsRequired()
					.HasDefaultValue(false);

			modelBuilder.Entity<Post>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Post>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedNever();
		}
		private static void CommentModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Comment>()
					.ToTable("Comments");

			modelBuilder.Entity<Comment>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Comment>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Comment>()
					.Property(x => x.AuthorId)
					.IsRequired();

			modelBuilder.Entity<Comment>()
					.Property(x => x.ParentCommentId);
					
			modelBuilder.Entity<Comment>()
					.Property(x => x.PostId);

			modelBuilder.Entity<Comment>()
					.Property(x => x.CollectibleId);

			modelBuilder.Entity<Comment>()
					.Property(x => x.ReviewId);

			modelBuilder.Entity<Comment>()
					.Property(x => x.Content)
					.IsRequired()
					.HasMaxLength(2048);

			modelBuilder.Entity<Comment>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Comment>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedNever();

			modelBuilder.Entity<Comment>()
					.Property(x => x.IsHidden)
					.IsRequired()
					.HasDefaultValue(false);
		}
		private static void FilterModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Filter>()
					.ToTable("Filters");

			modelBuilder.Entity<Filter>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Filter>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Filter>()
					.Property(x => x.CommunityId)
					.IsRequired();

			modelBuilder.Entity<Filter>()
					.Property(x => x.Name)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<Filter>()
					.Property(x => x.FilterTypeId)
					.IsRequired();
		}
		private static void FilterTypeModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<FilterType>()
					.ToTable("FilterTypes");

			modelBuilder.Entity<FilterType>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<FilterType>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<FilterType>()
					.Property(x => x.Name)
					.IsRequired()
					.HasMaxLength(50);
		}
		private static void PostTagModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostTag>()
					.ToTable("PostTags");

			modelBuilder.Entity<PostTag>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<PostTag>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<PostTag>()
					.Property(x => x.PostId)
					.IsRequired();

			modelBuilder.Entity<PostTag>()
					.Property(x => x.TagId)
					.IsRequired();
		}
		private static void ReactionModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Reaction>()
					.ToTable("Reactions");

			modelBuilder.Entity<Reaction>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Reaction>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Reaction>()
					.Property(x => x.PostId);

			modelBuilder.Entity<Reaction>()
					.Property(x => x.CommentId);

			modelBuilder.Entity<Reaction>()
					.Property(x => x.CollectibleId);

			modelBuilder.Entity<Reaction>()
					.Property(x => x.UserId)
					.IsRequired();

			modelBuilder.Entity<Reaction>()
					.Property(x => x.ReactionTypeId)
					.IsRequired();

			modelBuilder.Entity<Reaction>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Reaction>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedNever();
		}

		private static void TagModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Tag>()
					.ToTable("Tags");

			modelBuilder.Entity<Tag>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Tag>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Tag>()
					.Property(x => x.CommunityId)
					.IsRequired();

			modelBuilder.Entity<Tag>()
					.Property(x => x.Name)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<Tag>()
					.Property(x => x.Value)
					.IsRequired()
					.HasMaxLength(50);
		}
		private static void RelationshipBuilder(ModelBuilder modelBuilder)
		{
			// One user has many roles
			modelBuilder.Entity<User>()
				.HasMany(x => x.Roles)
				.WithOne(x => x.User)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);
			// One role has one user

			// One community has many roles
			modelBuilder.Entity<Community>()
				.HasMany(x => x.Roles)
				.WithOne(x => x.Community)
				.HasForeignKey(x => x.CommunityId)
				.OnDelete(DeleteBehavior.Cascade);
			// One role has one community

			// One user has many collectibles
			modelBuilder.Entity<User>()
				.HasMany(x => x.Collectibles)
				.WithOne(x => x.Owner)
				.HasForeignKey(x => x.OwnerId)
				.OnDelete(DeleteBehavior.Cascade);
			// One collectible has one user

			// One user has many auction
			modelBuilder.Entity<User>()
				.HasMany(x => x.Auctions)
				.WithOne(x => x.Auctioneer)
				.HasForeignKey(x => x.AuctioneerId)
				.OnDelete(DeleteBehavior.Cascade);
			// One auction has one user

			// One user has many bid
			modelBuilder.Entity<User>()
				.HasMany(x => x.Bids)
				.WithOne(x => x.Bidder)
				.HasForeignKey(x => x.BidderId)
				.OnDelete(DeleteBehavior.Cascade);
			// One bid has one user

			// One user has many exchange
			modelBuilder.Entity<User>()
				.HasMany(x => x.Exchanges)
				.WithOne(x => x.Exchanger)
				.HasForeignKey(x => x.ExchangerId)
				.OnDelete(DeleteBehavior.Cascade);
			// One exchange has one user

			// One user has many sale
			modelBuilder.Entity<User>()
				.HasMany(x => x.Sales)
				.WithOne(x => x.Vendor)
				.HasForeignKey(x => x.VendorId)
				.OnDelete(DeleteBehavior.Cascade);
			// One sale has one user

			// One user has many review
			modelBuilder.Entity<User>()
				.HasMany(x => x.Reviews)
				.WithOne(x => x.Reviewer)
				.HasForeignKey(x => x.ReviewerId)
				.OnDelete(DeleteBehavior.Cascade);
			// One review has one user

			// One user has many reaction
			modelBuilder.Entity<User>()
				.HasMany(x => x.Reactions)
				.WithOne(x => x.User)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);
			// One reaction has one user

			// One media has one user
			modelBuilder.Entity<MediaElement>()
				.HasOne(x => x.Uploader)
				.WithMany(x => x.MediaElements)
				.HasForeignKey(x => x.UploaderId)
				.OnDelete(DeleteBehavior.Cascade);
			// One user has many media

			// One community has many filter
			modelBuilder.Entity<Community>()
				.HasMany(x => x.CommunityFilters)
				.WithOne(x => x.Community)
				.HasForeignKey(x => x.CommunityId)
				.OnDelete(DeleteBehavior.Cascade);
			// One filter has one community

			// One community has many tag
			modelBuilder.Entity<Community>()
				.HasMany(x => x.CommunityTags)
				.WithOne(x => x.Community)
				.HasForeignKey(x => x.CommunityId)
				.OnDelete(DeleteBehavior.Cascade);
			// One tag has one community

			// One community has many post
			modelBuilder.Entity<Community>()
				.HasMany(x => x.Posts)
				.WithOne(x => x.Community)
				.HasForeignKey(x => x.CommunityId)
				.OnDelete(DeleteBehavior.Cascade);
			// One post has one community

			// One community has many auction
			modelBuilder.Entity<Community>()
				.HasMany(x => x.Auctions)
				.WithOne(x => x.Community)
				.HasForeignKey(x => x.CommunityId)
				.OnDelete(DeleteBehavior.Cascade);
			// One auction has one community
			// 
			// One community has many exchange
			modelBuilder.Entity<Community>()
				.HasMany(x => x.Exchanges)
				.WithOne(x => x.Community)
				.HasForeignKey(x => x.CommunityId)
				.OnDelete(DeleteBehavior.Cascade);
			// One exchange has one community
			// 
			// One community has many sale
			modelBuilder.Entity<Community>()
				.HasMany(x => x.Sales)
				.WithOne(x => x.Community)
				.HasForeignKey(x => x.CommunityId)
				.OnDelete(DeleteBehavior.Cascade);
			// One sale has one community
			// 
			// 
			// One bid has one auction
			modelBuilder.Entity<Bid>()
				.HasOne(x => x.Auction)
				.WithMany(x => x.Bids)
				.HasForeignKey(x => x.AuctionId)
				.OnDelete(DeleteBehavior.Cascade);
			// One auction has many bid

			// One auction has one collectible
			modelBuilder.Entity<Auction>()
				.HasOne(x => x.Collectible)
				.WithOne(x => x.Auction)
				.HasForeignKey<Collectible>(x => x.AuctionId)
				.OnDelete(DeleteBehavior.SetNull);
			// One collectible has zero or one auction


			// One sale has one collectible
			modelBuilder.Entity<Sale>()
				.HasOne(x => x.Collectible)
				.WithOne(x => x.Sale)
				.HasForeignKey<Collectible>(x => x.SaleId)
				.OnDelete(DeleteBehavior.Restrict);
			// One collectible has zero or one sale

			// One sale has one user (buyer)
			modelBuilder.Entity<Sale>()
			.HasOne(x => x.Buyer)
			.WithMany(x => x.Purchases)
			.HasForeignKey(x => x.BuyerId)
			.OnDelete(DeleteBehavior.SetNull);
			// One user has many sale as buyer

			// One review has one collectible
			modelBuilder.Entity<Review>()
                .HasOne(x => x.ReviewedCollectible)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.CollectibleId)
                .OnDelete(DeleteBehavior.Cascade);
			// One collectible has many reviews

			// One exchange has one collectible
			modelBuilder.Entity<Exchange>()
				.HasOne(x => x.Collectible)
				.WithOne(x => x.Exchange)
				.HasForeignKey<Collectible>(x => x.ExchangeId)
				.OnDelete(DeleteBehavior.SetNull);
			// One collectible has zero or one exchange

			// One post has one user (author)
			modelBuilder.Entity<Post>()
				.HasOne(x => x.Author)
				.WithMany(x => x.Posts)
				.HasForeignKey(x => x.AuthorId)
				.OnDelete(DeleteBehavior.Cascade);
			// One user has many posts

			// One comment has one user (author)
			modelBuilder.Entity<Comment>()
				.HasOne(x => x.Author)
				.WithMany(x => x.Comments)
				.HasForeignKey(x => x.AuthorId)
				.OnDelete(DeleteBehavior.Cascade);
			// One user has many comments

			// One post has many posttag
			modelBuilder.Entity<Post>()
				.HasMany(x => x.PostTags)
				.WithOne(x => x.Post)
				.HasForeignKey(x => x.PostId)
				.OnDelete(DeleteBehavior.Cascade);
			// One posttag has one post

			// One filter has one filtertype
			modelBuilder.Entity<Filter>()
				.HasOne(x => x.FilterType)
				.WithMany(x => x.Filters)
				.HasForeignKey(x => x.FilterTypeId)
				.OnDelete(DeleteBehavior.Cascade);
			// One filtertype has many filter

			// One collectible has many media
			modelBuilder.Entity<Collectible>()
				.HasMany(x => x.MediaElements)
				.WithOne(x => x.Collectible)
				.HasForeignKey(x => x.CollectibleId)
				.OnDelete(DeleteBehavior.Cascade);
			// One media has one collectible

			// One collectible has many comments
			modelBuilder.Entity<Collectible>()
				.HasMany(x => x.Comments)
				.WithOne(x => x.Collectible)
				.HasForeignKey(x => x.CollectibleId)
				.OnDelete(DeleteBehavior.Cascade);
			// One comment has one collectible

			// One post has many comments
			modelBuilder.Entity<Post>()
				.HasMany(x => x.Comments)
				.WithOne(x => x.Post)
				.HasForeignKey(x => x.PostId)
				.OnDelete(DeleteBehavior.Cascade);
			// One comment has one post

			// One review has many comments
			modelBuilder.Entity<Review>()
				.HasMany(x => x.Comments)
				.WithOne(x => x.Review)
				.HasForeignKey(x => x.ReviewId)
				.OnDelete(DeleteBehavior.Cascade);
			// One comment has one review

			// One post has many media
			modelBuilder.Entity<Post>()
				.HasMany(x => x.MediaElements)
				.WithOne(x => x.Post)
				.HasForeignKey(x => x.PostId)
				.OnDelete(DeleteBehavior.Cascade);
			// One media has one post

			// One user profile has many media
			modelBuilder.Entity<User>()
				.HasMany(x => x.ProfileMediaElements)
				.WithOne(x => x.Profile)
				.HasForeignKey(x => x.ProfileId)
				.OnDelete(DeleteBehavior.Cascade);
			// One media has one user profile

			// One post has many reactions
			modelBuilder.Entity<Post>()
				.HasMany(x => x.Reactions)
				.WithOne(x => x.Post)
				.HasForeignKey(x => x.PostId)
				.OnDelete(DeleteBehavior.Cascade);
			// One reaction has one post

			// One comment has many reactions
			modelBuilder.Entity<Comment>()
				.HasMany(x => x.Reactions)
				.WithOne(x => x.Comment)
				.HasForeignKey(x => x.CommentId)
				.OnDelete(DeleteBehavior.Cascade);
			// One reaction has one comment

			// One collectible has many reactions
			modelBuilder.Entity<Collectible>()
				.HasMany(x => x.Reactions)
				.WithOne(x => x.Collectible)
				.HasForeignKey(x => x.CollectibleId)
				.OnDelete(DeleteBehavior.Cascade);
			// One reaction has one collectible
		}

		public void RunSqlScript(string sqlScript)
		{
			var sql = @"
			USE prod;

			-- Create triggers for updating the updated_at column on ITimestamped entities

			DROP TRIGGER IF EXISTS update_role;
			CREATE TRIGGER update_role
			BEFORE UPDATE ON roles
			FOR EACH ROW
			SET NEW.updated_at = NOW();

			
			DROP TRIGGER IF EXISTS update_auction;
			CREATE TRIGGER update_auction
			BEFORE UPDATE ON auctions
			FOR EACH ROW
			SET NEW.updated_at = NOW();

			DROP TRIGGER IF EXISTS update_exchange;
			CREATE TRIGGER update_exchange
			BEFORE UPDATE ON exchanges
			FOR EACH ROW
			SET NEW.updated_at = NOW();

			DROP TRIGGER IF EXISTS update_sale;
			CREATE TRIGGER update_sale
			BEFORE UPDATE ON sales
			FOR EACH ROW
			SET NEW.updated_at = NOW();

			DROP TRIGGER IF EXISTS update_collectible;
			CREATE TRIGGER update_collectible
			BEFORE UPDATE ON collectibles
			FOR EACH ROW
			SET NEW.updated_at = NOW();

			DROP TRIGGER IF EXISTS update_review;
			CREATE TRIGGER update_review
			BEFORE UPDATE ON reviews
			FOR EACH ROW
			SET NEW.updated_at = NOW();

			DROP TRIGGER IF EXISTS update_bid;
			CREATE TRIGGER update_bid
			BEFORE UPDATE ON bids
			FOR EACH ROW
			SET NEW.updated_at = NOW();

			DROP TRIGGER IF EXISTS update_media_element;
			CREATE TRIGGER update_media_element
			BEFORE UPDATE ON media_elements
			FOR EACH ROW
			SET NEW.updated_at = NOW();

			DROP TRIGGER IF EXISTS update_post;
			CREATE TRIGGER update_post
			BEFORE UPDATE ON posts
			FOR EACH ROW
			SET NEW.updated_at = NOW();

			DROP TRIGGER IF EXISTS update_comment;
			CREATE TRIGGER update_comment
			BEFORE UPDATE ON comments
			FOR EACH ROW
			SET NEW.updated_at = NOW();

			DROP TRIGGER IF EXISTS update_reaction;
			CREATE TRIGGER update_reaction
			BEFORE UPDATE ON reactions
			FOR EACH ROW
			SET NEW.updated_at = NOW();
			";

			var commands = sql.Split(new[] { ";\r\n", ";\n" }, StringSplitOptions.RemoveEmptyEntries);

			using var transaction = Database.BeginTransaction();

			try
			{
				foreach (var command in commands)
				{
					var trimmedCommand = command.Trim();
					if (!string.IsNullOrEmpty(trimmedCommand))
					{
						Database.ExecuteSqlRaw(trimmedCommand);
					}
				}

				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}
	}
}