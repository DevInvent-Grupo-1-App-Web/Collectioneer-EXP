using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Configuration;

namespace Collectioneer.API.Shared.Infrastructure.Repositories
{
	public class UnitOfWork(AppDbContext context) : IUnitOfWork
	{
		public async Task CompleteAsync()
		{
			await context.SaveChangesAsync();
		}
	}
}