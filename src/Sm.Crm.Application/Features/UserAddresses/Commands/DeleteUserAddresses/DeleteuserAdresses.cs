using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.UserAddresses.Commands.DeleteUserAddresses;
public record DeleteuserAdressesCommand(int Id): IRequest<bool>;
public class DeleteuserAdressesCommanddHandler : IRequestHandler<DeleteuserAdressesCommand, bool>
{
    private readonly IUserAddressRepository _repository;
    private readonly IMapper _mapper;

    public DeleteuserAdressesCommanddHandler(IUserAddressRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteuserAdressesCommand request, CancellationToken cancellationToken)
    {
        bool isSuccess = await _repository.DeleteById(request.Id);
        return isSuccess;
    }
}
