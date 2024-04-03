using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Titles.Queries.GetAllTitles;

public class GetTitleByIdQuery : IRequest<TitleDto?>
{
    public int Id { get; set; }

    public GetTitleByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetTitleByIdQueryHandler : IRequestHandler<GetTitleByIdQuery, TitleDto?>
{
    private readonly ITitleRepository _repository;
    private readonly IMapper _mapper;

    public GetTitleByIdQueryHandler(ITitleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TitleDto?> Handle(GetTitleByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        return _mapper.Map<TitleDto>(entity);
    }
}