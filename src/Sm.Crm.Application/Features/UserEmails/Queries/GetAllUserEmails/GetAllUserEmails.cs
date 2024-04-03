using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.UserEmails.Queries.GetAllUserEmails;

public record GetAllUserEmailsQuery : IRequest<ICollection<UserEmailDto>>;

public class GetAllUserEmailsQueryHandler : IRequestHandler<GetAllUserEmailsQuery, ICollection<UserEmailDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserEmailRepository _userEmailRepository;

    public GetAllUserEmailsQueryHandler(IMapper mapper, IUserEmailRepository userEmailRepository)
    {
        _mapper = mapper;
        _userEmailRepository = userEmailRepository;
    }

    public async Task<ICollection<UserEmailDto>> Handle(GetAllUserEmailsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _userEmailRepository.GetAll().ToListAsync();
        return _mapper.Map<List<UserEmailDto>>(entities).ToList();
    }
}