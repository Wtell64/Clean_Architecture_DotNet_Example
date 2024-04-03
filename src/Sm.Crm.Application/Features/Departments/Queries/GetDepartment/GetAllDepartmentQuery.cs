using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Departments.Queries.GetDepartment;
public record GetAllDepartmentQuery : IRequest<ICollection<DepartmentDto>>;

public class GetAllDepartmentQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllDepartmentQuery, ICollection<DepartmentDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ICollection<DepartmentDto>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.DepartmentRepository.GetAll().ToListAsync();
        return _mapper.Map<List<DepartmentDto>>(entities).ToList();
    }
}