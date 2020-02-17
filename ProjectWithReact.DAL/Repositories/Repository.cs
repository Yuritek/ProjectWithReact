using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectWithReact.DAL.Interface;

namespace ProjectWithReact.DAL.Repositories
{
	public sealed class Repository<TEntity> : IRepository<TEntity>
		where TEntity : class, new()
	{
		private readonly DbContext _unitOfWork;
		private DbSet<TEntity> _entities;
		private DbSet<TEntity> Entities => _entities ?? (_entities = _unitOfWork.Set<TEntity>());

		public Repository(DbContext unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public TEntity GetById(int id)
		{
			return Entities.Find(id);
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

		public void Add(TEntity entity)
		{
			if (entity == null)
			{
				throw new ArgumentException("entity");
			}

			Entities.Add(entity);
		}

		public void Clear()
		{
			TEntity[] entities = Entities.ToArray();
			RemoveRange(entities);
		}

		public void Delete(int entity)
		{
			TEntity existing = GetById(entity);
			if (existing != null) Entities.Remove(existing);
		}

		private void RemoveRange(TEntity[] entities)
		{
			foreach (TEntity entity in entities)
			{
				if (_unitOfWork.Entry(entity).State == EntityState.Detached)
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
		}
	}
}