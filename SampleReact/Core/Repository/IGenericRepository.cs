using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleReact.Core.Repository
{
	public interface IGenericRepository<TEntity> where TEntity : class, new()
	{
		IQueryable<TEntity> Entities { get; }
		void Add(TEntity entity);
		void Delete(int entity);
		void Clear();
		void Update(TEntity employee);
		Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null,
			int take = 0,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
	}
}