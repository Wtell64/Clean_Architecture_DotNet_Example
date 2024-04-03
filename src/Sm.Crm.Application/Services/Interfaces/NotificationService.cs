using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Application.Services.Interfaces;
public class NotificationService : INotificationService
{
    private readonly IApplicationDbContext _db;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public NotificationService(IApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _db = db;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<NotificationDto>>> GetAll()
    {
        var entities = _unitOfWork.NotificationRepository.GetAll();
        return Result<List<NotificationDto>>.Success(_mapper.Map<List<NotificationDto>>(entities).ToList());
    }

    public async Task<PaginatedResult<NotificationDto>> GetPaginated(PaginationRequest req)
    {
        var entityQuery = _unitOfWork.NotificationRepository.GetAll()
           .Include(e => e.UserId)
           .OrderByDescending(c => c.Id);

        var totalEntity = await entityQuery.CountAsync();
        var pagedEntities = await entityQuery.Skip((req.PageNumber - 1) * req.PageSize).AsNoTracking().ToListAsync();
        var pagedDtos = _mapper.Map<List<NotificationDto>>(pagedEntities);

        return new PaginatedResult<NotificationDto>(pagedDtos, totalEntity, req.PageNumber, req.PageSize);
    }

    public async Task<Result<NotificationDto?>> GetById(int id)
    {
        var entity = await _unitOfWork.NotificationRepository.GetById(id);
        return Result<NotificationDto?>.Success(_mapper.Map<NotificationDto>(entity));
    }

    public async Task<Result<int>> Create(CreateOrEditNotificationDto notification)
    {
        var entity = _mapper.Map<Notification>(notification);
        var id = await _unitOfWork.NotificationRepository.Create(entity);
        await _unitOfWork.CommitAsync();
        return Result<int>.Success(id);
    }

    public async Task<Result<bool>> Update(CreateOrEditNotificationDto notification)
    {
        var entity = _mapper.Map<Notification>(notification);
        bool isSuccess = await _unitOfWork.NotificationRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Updated!");
        else
            return Result<bool>.Failure("Not updated!");
    }

    public async Task<Result<bool>> Delete(int id)
    {
        bool isSuccess = await _unitOfWork.NotificationRepository.DeleteById(id);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Deleted!");
        else
            return Result<bool>.Failure("Not deleted!");
    }
}
