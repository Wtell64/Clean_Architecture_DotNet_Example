using AutoMapper;
using MediatR;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Features.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommand : IRequest<int>
{
    public string? Name { get; set; }
}

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, int>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateDepartmentCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Department>(request);
        await _db.Departments.AddAsync(entity);
        await _db.SaveChangesAsync();
        return entity.Id;
    }
}
