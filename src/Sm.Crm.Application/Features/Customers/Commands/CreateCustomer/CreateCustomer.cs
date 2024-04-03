using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;
using Sm.Crm.Domain.Events;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<long>
{
    public string? IdentityNumber { get; set; }
    public CustomerTypeEnum? CustomerType { get; set; }
    public string? CompanyName { get; set; }
    public string? BirthDate { get; set; }
    public int? StatusTypeId { get; set; }
    public int? TitleId { get; set; }
    public int? TerritoryId { get; set; }
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, long>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IPublisher publisher)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<long> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Customer>(request);
        var id = await _customerRepository.Create(entity);

        //await _publisher.Publish(new CustomerCreatedEvent(entity));
        entity.AddDomainEvent(new CustomerCreatedEvent(entity));

        return id;
    }
}