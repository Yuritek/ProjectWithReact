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
		private DbSet<TEntity> _entities;
		private DbSet<TEntity> Entities => _entities ?? (_entities = _context.Set<TEntity>());

		public GenericGenericRepository(DbContext context)
		{
			_context = context;
		}

		public async Task<List<TEntity>> Get(
			Expression<Func<TEntity, bool>> filter = null,
			int take = 0,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
		{
			IQueryable<TEntity> query = Entities;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			var queryOrder = orderBy?.Invoke(query) ?? query;
			return await (take > 0 ? queryOrder.Take(take).ToListAsync() : queryOrder.ToListAsync());
		}

		public IQueryable<TEntity> Table => Entities;

		public void Add(TEntity entity)
		{
			if (entity == null)
			{
				throw new ArgumentException("entity");
			}

			Entities.Add(entity);
			_context.SaveChanges();
		}

		public void Clear()
		{
			TEntity[] entities = Entities.ToArray();
			RemoveRange(entities);
		}

		public void Delete(int entity)
		{
			var findEntity = Entities.Find(entity);
			Entities.Remove(findEntity);
			_context.SaveChanges();
		}

		private void RemoveRange(TEntity[] entities)
		{
			foreach (TEntity entity in entities)
			{
				if (_context.Entry(entity).State == EntityState.Detached)
				{
					Entities.Attach(entity);
				}

				Entities.Remove(entity);
			}
		}

		public void Update(TEntity entity)
		{
			if (entity == null)
			{
				throw new ArgumentException("entity");
			}

			Entities.Update(entity);
			_context.SaveChanges();
		}
	}
}