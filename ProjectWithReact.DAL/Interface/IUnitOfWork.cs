using System;

namespace ProjectWithReact.DAL.Interface
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new();
		void Commit();
	}
}