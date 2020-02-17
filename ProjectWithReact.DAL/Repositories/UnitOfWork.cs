using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectWithReact.DAL.Interface;

namespace ProjectWithReact.DAL.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private DbContext Context { get; }
		private readonly Dictionary<Type, object> _repositories;

		public UnitOfWork(DbContext context)
		{
			Context = context;
			_repositories = new Dictionary<Type, object>();
		}

		public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new()
		{
			if (_repositories.Keys.Contains(typeof(TEntity)))
				return _repositories[typeof(TEntity)] as IRepository<TEntity>;

			var repository = new Repository<TEntity>(this.Context);
			_repositories.Add(typeof(TEntity), repository);
			return repository;
		}

		public void Commit()
		{
			Context.SaveChanges();
		}

		#region IDisposable

		private bool _disposed;

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					Context.Dispose();
				}
			}

			_disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}