using SampleReact.Core.Repository;
using SampleReact.Models;

namespace SampleReact.Core.UnitOfWork
{
    public interface IUnitOfWorkDirectoryContext
    {
	   IGenericRepository<Contacts> ContactsGenericRepository { get; }
    }
}
