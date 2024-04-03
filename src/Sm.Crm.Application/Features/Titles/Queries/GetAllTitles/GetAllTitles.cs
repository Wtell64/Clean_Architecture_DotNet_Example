using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Constants;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Titles.Queries.GetAllTitles;

public record GetAllTitlesQuery : IRequest<ICollection<TitleDto>>;

public class GetAllTitleQueryHandler : IRequestHandler<GetAllTitlesQuery, ICollection<TitleDto>>
{
    private readonly ITitleRepository _repository;
    private readonly IMapper _mapper;
    private readonly IAppCache _redisCache;

    public GetAllTitleQueryHandler(ITitleRepository repository, IMapper mapper, IAppCache redisCache)
    {
        _repository = repository;
        _mapper = mapper;
        _redisCache = redisCache;
    }

    public async Task<ICollection<TitleDto>?> Handle(GetAllTitlesQuery request, CancellationToken cancellationToken)
    {
        var data = await _redisCache.GetOrSetCache(CacheConstants.Titles, async () =>
        {
            var entities = await _repository.GetAll().ToListAsync();
            return _mapper.Map<List<TitleDto>>(entities);
        });

        if (data != null) return await data;
        return null;
    }
}