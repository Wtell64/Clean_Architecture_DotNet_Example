using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Services;

public class TaskService : ITaskService
{
    private readonly IApplicationDbContext _db;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskService(IApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _db = db;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Create(CreateOrEditTaskDto dto)
    {
        try
        {
            var entity = _mapper.Map<TaskItem>(dto);
            var id = await _unitOfWork.TaskRepository.Create(entity);
            await _unitOfWork.CommitAsync();
            return Result<int>.Success(id);
        }
        catch (Exception)
        {
            return Result<int>.Failure("A problem occured when creating the task");
        }
    }

    public async Task<Result<bool>> Delete(int id)
    {
        bool isSuccess = await _unitOfWork.TaskRepository.DeleteById(id);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Task deleted succesfully!");
        else
            return Result<bool>.Failure("A problem occured when deleting the task!");
    }

    public async Task<Result<List<TaskDto>>> GetAll()
    {
        var entities = await _unitOfWork.TaskRepository.GetAll().ToListAsync();
        return Result<List<TaskDto>>.Success(_mapper.Map<List<TaskDto>>(entities).ToList());
    }

    public async Task<Result<TaskDto?>> GetById(int id)
    {
        var entity = await _unitOfWork.TaskRepository.GetById(id);
        return Result<TaskDto?>.Success(_mapper.Map<TaskDto>(entity));
    }

    public async Task<Result<CreateOrEditTaskDto?>> GetFormById(int id)
    {
        var entity = await _unitOfWork.TaskRepository.GetById(id);
        return Result<CreateOrEditTaskDto?>.Success(_mapper.Map<CreateOrEditTaskDto>(entity));
    }

    public async Task<PaginatedResult<TaskDto>> GetPaginated(PaginationRequest req)
    {
        var entities = _db.Tasks
        .Include(e => e.TaskStatusFk)
        .Include(e => e.RequestFk)
        .Include(e => e.EmployeeUserFk)
        .OrderByDescending(e => e.Id)
        .ProjectTo<TaskDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<TaskDto>.Create(entities.AsNoTracking(), req.PageNumber, req.PageSize);
    }

    public async Task<Result<bool>> Update(CreateOrEditTaskDto dto)
    {
        var entity = _mapper.Map<TaskItem>(dto);
        bool isSuccess = await _unitOfWork.TaskRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Task updated succesfully!");
        else
            return Result<bool>.Failure("A error occured when updating the task!");
    }
}