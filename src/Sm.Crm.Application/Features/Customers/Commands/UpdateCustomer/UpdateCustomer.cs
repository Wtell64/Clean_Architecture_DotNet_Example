using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<bool>
{
    public long? Id { get; set; }
    public string? IdentityNumber { get; set; }
    public CustomerTypeEnum? CustomerType { get; set; }
    public string? CompanyName { get; set; }
    public string? BirthDate { get; set; }
    public int? StatusTypeId { get; set; }
    public int? TitleId { get; set; }
    public int? TerritoryId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public GenderEnum? Gender { get; set; }
}

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Customer>(request);
        bool isSuccess = await _customerRepository.Update(entity);
        return isSuccess;
    }
}