using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommand : IRequest<bool>
{
	public int Id { get; set; }
	public Guid? UserId { get; set; }
	public string IdentityNumber { get; set; } = null!;
	public int? DepartmentId { get; set; }
	public DateTime? StartDate { get; set; }
	public int? StatusTypeId { get; set; }
	public int? TerritoryId { get; set; }
	public DateOnly? BirthDate { get; set; }
	public Guid? ReportsToUserId { get; set; }
	public string? PhotoPath { get; set; }
}

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
{
	private readonly IEmployeeRepository _repository;
	private readonly IMapper _mapper;

	public UpdateEmployeeCommandHandler(IEmployeeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<Employee>(request);
		bool isSuccess = await _repository.Update(entity);
		return isSuccess;
	}
}