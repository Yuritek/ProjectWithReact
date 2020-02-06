using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleReact.Core.UnitOfWork;
using SampleReact.Models;

namespace SampleReact.Controllers
{
	public class ContactController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkDirectoryContext _directoryContext;

		public ContactController(IMapper mapper, IUnitOfWorkDirectoryContext directoryContext)
		{
			_mapper = mapper;
			_directoryContext = directoryContext;
		}

		[HttpGet]
		[Route("api/Contact/Index")]
		public async Task<IEnumerable<ContactsViewModel>> Index()
		{
			var user = await _directoryContext.ContactsGenericRepository.Table.ToListAsync();
			return _mapper.Map<IEnumerable<Contacts>, List<ContactsViewModel>>(user);
		}

		[HttpPost]
		[Route("api/Contact/Create")]
		public int Create(Contacts employee)
		{
			_directoryContext.ContactsGenericRepository.Add(employee);
			return employee.Code;
		}

		[HttpGet]
		[Route("api/Contact/Details/{id}")]
		public async Task<Contacts> Details(int id)
		{
			return await _directoryContext
				.ContactsGenericRepository
				.Table
				.FirstOrDefaultAsync(contacts => contacts.Code == id);
		}

		[HttpPut]
		[Route("api/Contact/Edit")]
		public int Edit(Contacts employee)
		{
			_directoryContext.ContactsGenericRepository.Update(employee);
			return employee.Code;
		}

		[HttpDelete]
		[Route("api/Contact/Delete/{id}")]
		public int Delete(int id)
		{
			_directoryContext.ContactsGenericRepository.Delete(id);
			return id;
		}
	}
}