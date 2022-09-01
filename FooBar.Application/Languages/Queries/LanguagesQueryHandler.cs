using System.Data;
using AutoMapper;
using Dapper;
using Domain.DistributedCache;
using FooBar.Domain.Entities;
using FooBar.Domain.Repository;
using MediatR;

namespace Application.Languages.Queries
{
    public class LanguagesQueryHandler : IRequestHandler<LanguagesQuery, IEnumerable<LanguageDto>>
    {
        private readonly IGenericRepository<CommonLanguage> _genericRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationCache<CommonLanguage> _cache;

        public LanguagesQueryHandler(IGenericRepository<CommonLanguage> genericRepository, IMapper mapper, IApplicationCache<CommonLanguage> cache)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<LanguageDto>> Handle(LanguagesQuery request, CancellationToken cancellationToken)
        {
            var languages = await _genericRepository.GetAsync();
            return _mapper.Map<IEnumerable<LanguageDto>>(languages);
        }
    }
}