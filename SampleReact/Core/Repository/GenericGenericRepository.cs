using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SampleReact.Core.Repository
{
	public sealed class GenericGenericRepository<TEntity> : IGenericRepository<TEntity>
		where TEntity : class, new()
	{
		private readonly DbContext _context;
		private DbSet<TEntity> DbSet => _context.Set<TEntity>();
		public GenericGenericRepository(DbContext context)
		{
			_context = context;
		}
		public async Task<List<TEntity>> Get(
			Expression<Func<TEntity, bool>> filter = null,
			int take = 0,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
		{
			IQueryable<TEntity> query = DbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}
			var queryOrder = orderBy?.Invoke(query) ?? query;
			return await (take > 0 ? queryOrder.Take(take).ToListAsync() : queryOrder.ToListAsync());
		}
		public IQueryable<TEntity> Entities => DbSet;

		public void Add(TEntity entity)
		{
			DbSet.Add(entity);
		}
		public void Clear()
		{
			TEntity[] entities = DbSet.ToArray();
			RemoveRange(entities);
		}

		public void Delete(int entity)
		{
			var findEntity = DbSet.Find(entity);
			DbSet.Remove(findEntity);
		}
		private void RemoveRange(TEntity[] entities)
		{
			foreach (TEntity entity in entities)
			{
				if (_context.Entry(entity).State == EntityState.Detached)
				{
					DbSet.Attach(entity);
				}
				DbSet.Remove(entity);
			}
		}
		public void Update(TEntity entity)
		{
			DbSet.Update(entity);
		}
	}
}
