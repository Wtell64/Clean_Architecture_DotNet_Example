using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.UserAddresses.Commands.UpdateUserAddresses;
public class UpdateUserAdressesCommand : IRequest<bool>
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public AddressTypeEnum? AddressType { get; set; }
}
public class UpdateUserAdressesCommandHandler : IRequestHandler<UpdateUserAdressesCommand, bool>
{
    private readonly IUserAddressRepository _repository;
    private readonly IMapper _mapper;

    public UpdateUserAdressesCommandHandler(IUserAddressRepository repository, IMapper mapper)
    {
       _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateUserAdressesCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<UserAddress>(request);
        bool isSuccess = await _repository.Update(entity);
        return isSuccess;
    }
}

