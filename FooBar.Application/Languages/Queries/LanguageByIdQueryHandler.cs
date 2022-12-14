using AutoMapper;
using Domain.DistributedCache;
using Application.Languages.Queries;
using FooBar.Domain.Entities;
using FooBar.Domain.Repository;
using MediatR;

namespace Application.Languages.Queries;
public class LanguageByIdQueryHandler : IRequestHandler<LanguageByIdQuery, LanguageDto>
{
    private readonly IGenericRepository<CommonLanguage> _genericRepository;
    private readonly IMapper _mapper;
    private readonly IApplicationCache<CommonLanguage> _cache;

    public LanguageByIdQueryHandler(IGenericRepository<CommonLanguage> genericRepository, IMapper mapper, IApplicationCache<CommonLanguage> cache)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<LanguageDto> Handle(LanguageByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheResult = await _cache.GetValueAsync(request.Id);
        if (cacheResult is not null)
        {
            return _mapper.Map<LanguageDto>(cacheResult);
        }
        else
        {
            await Task.Delay(3000);
            var language = await _genericRepository.GetByIdAsync(request.Id);
            await _cache.SetValueAsync(request.Id, language);
            return _mapper.Map<LanguageDto>(language);

        }

    }
}
