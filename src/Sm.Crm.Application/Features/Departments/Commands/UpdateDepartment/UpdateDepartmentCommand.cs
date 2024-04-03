using AutoMapper;
using MediatR;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Features.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, bool>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public UpdateDepartmentCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Department>(request);
        _db.Departments.Update(entity);
        return await _db.SaveChangesAsync(cancellationToken) > 0;
    }
}