using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Titles.Queries.GetAllTitles;

public class GetPaginatedTitlesQuery : IRequest<PaginatedResult<TitleDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPaginatedTitlesQueryHandler : IRequestHandler<GetPaginatedTitlesQuery, PaginatedResult<TitleDto>>
{
    private readonly ITitleRepository _repository;
    private readonly IMapper _mapper;

    public GetPaginatedTitlesQueryHandler(ITitleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<TitleDto>> Handle(GetPaginatedTitlesQuery request, CancellationToken cancellationToken)
    {
        var entities = _repository.GetAll()
             .OrderByDescending(e => e.Id)
             .ProjectTo<TitleDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<TitleDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }
}