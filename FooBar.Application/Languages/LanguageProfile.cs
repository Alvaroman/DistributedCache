using Application.Languages.Queries;
using AutoMapper;

namespace Application.Languages;
public class LanguageProfile : Profile
{
    public LanguageProfile()
    {
        CreateMap<FooBar.Domain.Entities.CommonLanguage, LanguageDto>().ReverseMap();
    }
}