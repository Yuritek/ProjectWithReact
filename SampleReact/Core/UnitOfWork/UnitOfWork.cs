using System;
using Microsoft.EntityFrameworkCore;
using SampleReact.Core.Repository;
using SampleReact.Models;

namespace SampleReact.Core.UnitOfWork
{
	public class UnitOfWork : IUnitOfWorkDirectoryContext, IDisposable
	{
		private readonly DbContext _directoryContext;
		private GenericGenericRepository<Contacts> _directoryGenericRepository;
		public UnitOfWork(DbContext context)
		{
			_directoryContext = context;
		}
		public UnitOfWork()
		{
			_directoryContext = new DirectoryContext();
		}
	   public IGenericRepository<Contacts> ContactsGenericRepository
	   {
			get
			{
				return _directoryGenericRepository = _directoryGenericRepository ?? new GenericGenericRepository<Contacts>(_directoryContext);
			}
		}
		#region IDisposable

		private bool _disposed;
		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_directoryContext.Dispose();
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
