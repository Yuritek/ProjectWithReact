using AutoMapper;
using ProjectWithReact.DAL.Entities;
using SampleReact.Models;

namespace SampleReact.Service
{
	public class MappingProfile : Profile
	{
		private string GetSuffix => "СПБ";

		public MappingProfile()
		{
			CreateMap<Contacts, ContactsViewModel>()
				.ForMember("CodeEx", opt => opt.MapFrom(src => $"{GetSuffix}{src.Code:D5}"));
		}
	}
}
