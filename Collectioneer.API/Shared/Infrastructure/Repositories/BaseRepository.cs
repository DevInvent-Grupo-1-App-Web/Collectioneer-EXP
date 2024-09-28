using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Shared.Infrastructure.Repositories
{
	public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T>
	where T : class
	{
		protected readonly AppDbContext _context = context;

		public async Task<T> Add(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			return entity;
		}

		public async Task Delete(int id)
		{
			var entity = await _context.Set<T>().FindAsync(id) ??
					throw new EntityNotFoundException($"Entity with id {id} wasn't found in the server.");

			_context.Set<T>().Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<T?> GetById(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<T> Update(T entity)
		{
			if (entity is ITimestamped timestampedEntity)
			{
				timestampedEntity.UpdatedAt = DateTime.UtcNow;
			}

			_context.Set<T>().Update(entity);
			await _context.SaveChangesAsync();
			return entity;
		}
	}
}