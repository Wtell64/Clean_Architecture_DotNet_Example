using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Customers.Commands.CreateCustomer;

public class CreateUserEmailCommand : IRequest<long>
{
    public Guid UserId { get; set; }
    public string? EmailAddress { get; set; }
    public EmailTypeEnum EmailType { get; set; }
}

public class CreateUserEmailCommandHandler : IRequestHandler<CreateUserEmailCommand, long>
{
    private readonly IUserEmailRepository _emailRepository;
    private readonly IMapper _mapper;

    public CreateUserEmailCommandHandler(IUserEmailRepository emailRepository, IMapper mapper)
    {
        _emailRepository = emailRepository;
        _mapper = mapper;
    }

    public async Task<long> Handle(CreateUserEmailCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<UserEmail>(request);
        var id = await _emailRepository.Create(entity);
        return id;
    }
}