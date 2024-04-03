using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.UserEmails.Queries.GetUserEmailsById;


public class GetUserEmailByIdQuery : IRequest<UserEmailDto?>
{
    public long Id { get; set; }

    public GetUserEmailByIdQuery(long id)
    {
        Id = id;
    }
}

public class GetUserEmailByIdQueryHandler : IRequestHandler<GetUserEmailByIdQuery, UserEmailDto?>
{
    private readonly IMapper _mapper;
    private readonly IUserEmailRepository _userEmailRepository;

    public GetUserEmailByIdQueryHandler( IMapper mapper , IUserEmailRepository userEmailRepository)
    {
     
        _mapper = mapper;
        _userEmailRepository= userEmailRepository;
    }

    public async Task<UserEmailDto?> Handle(GetUserEmailByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _userEmailRepository
            .GetAll(e => e.EmailAddress)
            .FirstOrDefaultAsync(e => e.Id == request.Id);
        return _mapper.Map<UserEmailDto>(entity);
    }
}