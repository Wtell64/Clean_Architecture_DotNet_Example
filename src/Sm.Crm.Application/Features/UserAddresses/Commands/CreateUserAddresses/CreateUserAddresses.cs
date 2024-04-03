using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;
using Sm.Crm.Domain.Events;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.UserAddresses.Commands.CreateUserAddresses;
public class CreateUserAddressesCommand : IRequest<int>
{
    public Guid? UserId { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public AddressTypeEnum? AddressType { get; set; }

    public class CreateUserAddressesCommandHandler : IRequestHandler<CreateUserAddressesCommand, int>
    {
        readonly IUserAddressRepository _repository;
        readonly IMapper _mapper;
        readonly IPublisher _publisher;

        public CreateUserAddressesCommandHandler(IUserAddressRepository repository, IMapper mapper, IPublisher publisher)
        {
           _repository = repository;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<int> Handle(CreateUserAddressesCommand request, CancellationToken cancellationToken)
        {
            //request.UserId = Guid.Parse("c167b62a-c0eb-4f15-1e80-08dc37dee4f7");
            var entity = _mapper.Map<UserAddress>(request);
          
            var id = await _repository.Create(entity);
            entity.AddDomainEvent(new UserAdressesCreatedEvents(entity));
            return id;
        }
    }
}
