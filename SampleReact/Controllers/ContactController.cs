using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectWithReact.DAL.Entities;
using ProjectWithReact.DAL.Interface;
using SampleReact.Models;

namespace SampleReact.Controllers
{
	public class ContactController : Controller
	{
		private readonly IMapper _mapper;

		private readonly IUnitOfWork _directoryContext;

		private IRepository<Contacts> _repo
		{
			get { return _directoryContext.GetRepository<Contacts>(); }
		}

		public ContactController(IMapper mapper, IUnitOfWork directoryContext)
		{
			_mapper = mapper;
			_directoryContext = directoryContext;
		}

		[HttpGet]
		[Route("api/Contact/Index")]
		public async Task<IEnumerable<ContactsViewModel>> Index()
		{
			var user = await _repo.Get();
			return _mapper.Map<IEnumerable<Contacts>, List<ContactsViewModel>>(user);
		}

		[HttpPost]
		[Route("api/Contact/Create")]
		public int Create(Contacts employee)
		{
			_repo.Add(employee);
			_directoryContext.Commit();
		  return employee.Code;
		}

		[HttpGet]
		[Route("api/Contact/Details/{id}")]
		public async Task<Contacts> Details(int id)
		{
			var result = await _repo
				.Get(contacts => contacts.Code == id, 1);
			return result.FirstOrDefault();
		}

		[HttpPut]
		[Route("api/Contact/Edit")]
		public int Edit(Contacts employee)
		{
			_repo.Update(employee);
			_directoryContext.Commit();
			return employee.Code;
		}

		[HttpDelete]
		[Route("api/Contact/Delete/{id}")]
		public int Delete(int id)
		{
			_repo.Delete(id);
			_directoryContext.Commit();
		  return id;
		}
	}
}