using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectWithReact.DAL.Interface
{
	public interface IRepository<TEntity> where TEntity : class, new()
	{
		
		void Add(TEntity entity);
		void Delete(int entity);
		void Clear();
		void Update(TEntity employee);

		TEntity GetById(int Id);

	   Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null,
			int take = 0,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
	}
}