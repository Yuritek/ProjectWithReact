using AutoMapper;

namespace SampleReact.Models
{
	public class ContactsViewModel
	{
		public int Code { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Patronymic { get; set; }
		public string Email { get; set; }

		public string Phone { get; set; }

		public string CodeEx { get; set; }

		public string FullName => $"{Surname} {Name} {Patronymic}";
	}
}