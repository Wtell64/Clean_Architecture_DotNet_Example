using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Constants;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Titles.Commands.CreateTitle;

public class CreateTitleCommand : IRequest<int>
{
    public string? Name { get; set; }
}

public class CreateTitleCommandHandler : IRequestHandler<CreateTitleCommand, int>
{
    private readonly ITitleRepository _repository;
    private readonly IMapper _mapper;

    public CreateTitleCommandHandler(ITitleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateTitleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Title>(request);
        var id = await _repository.Create(entity);
        return id;
    }
}