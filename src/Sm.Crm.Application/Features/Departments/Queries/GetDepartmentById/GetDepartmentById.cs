using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Departments.Queries.GetDepartmentById;

public record GetDepartmentById(int Id) : IRequest<DepartmentDto?>;

public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentById, DepartmentDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDepartmentByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DepartmentDto?> Handle(GetDepartmentById request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.DepartmentRepository.GetById(request.Id);
        return _mapper.Map<DepartmentDto>(entity);
    }
}