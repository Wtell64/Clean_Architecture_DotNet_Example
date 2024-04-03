using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.UserEmails.Commands.UpdateEmails;

public class UpdateUserEmailCommand : IRequest<bool>
{
    public int? Id { get; set; }
    public Guid UserId { get; set; }
    public string? EmailAddress { get; set; }
    public EmailTypeEnum EmailType { get; set; }
}

public class UpdateUserEmailCommandHandler : IRequestHandler<UpdateUserEmailCommand, bool>
{
    private readonly IUserEmailRepository _userEmailRepository;
    private readonly IMapper _mapper;

    public UpdateUserEmailCommandHandler(IUserEmailRepository userEmailRepository, IMapper mapper)
    {
        _userEmailRepository = userEmailRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<UserEmail>(request);
        bool isSuccess = await _userEmailRepository.Update(entity);
        return isSuccess;
    }
}