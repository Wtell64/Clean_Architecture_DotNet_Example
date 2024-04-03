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

public class CustomerService : ICustomerService
{
    private readonly IApplicationDbContext _db;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerService(IApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _db = db;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<CustomerDto>>> GetAll()
    {
        var entities = await _unitOfWork.CustomerRepository.GetAllWithUser();
        return Result<List<CustomerDto>>.Success(_mapper.Map<List<CustomerDto>>(entities).ToList());
    }

    public async Task<PaginatedResult<CustomerDto>> GetPaginatedNormal(PaginationRequest req)
    {
        var entityQuery = _unitOfWork.CustomerRepository.GetAll()
           .Include(e => e.UserFk)
           .OrderByDescending(c => c.Id);

        var totalEntity = await entityQuery.CountAsync(); // // SELECT count(*) FROM Customers
        //var entities = await entityQuery.ToListAsync(); // SELECT * FROM Customers
        var pagedEntities = await entityQuery.Skip((req.PageNumber - 1) * req.PageSize).AsNoTracking().ToListAsync();
        var pagedDtos = _mapper.Map<List<CustomerDto>>(pagedEntities);

        return new PaginatedResult<CustomerDto>(pagedDtos, totalEntity, req.PageNumber, req.PageSize);
    }

    public async Task<PaginatedResult<CustomerDto>> GetPaginated(PaginationRequest req)
    {
        var entities = _unitOfWork.CustomerRepository.GetAll()
             .Include(e => e.UserFk)
             .Include(e => e.TitleFk)
             .Include(e => e.StatusTypeFk)
             .Include(e => e.TerritoryFk)
             .OrderByDescending(e => e.Id)
             .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<CustomerDto>.Create(entities.AsNoTracking(), req.PageNumber, req.PageSize);
    }

    public async Task<Result<CustomerDto?>> GetById(long id)
    {
        var entity = await _unitOfWork.CustomerRepository
            .GetAll(e => e.UserFk, e => e.TitleFk, e => e.StatusTypeFk, e => e.TerritoryFk)
            .FirstOrDefaultAsync(e => e.Id == id);
        return Result<CustomerDto?>.Success(_mapper.Map<CustomerDto>(entity));
    }

    public async Task<Result<CreateOrUpdateCustomerDto?>> GetFormById(long id)
    {
        var entity = await _unitOfWork.CustomerRepository.GetById(id);
        return Result<CreateOrUpdateCustomerDto?>.Success(_mapper.Map<CreateOrUpdateCustomerDto>(entity));
    }

    public async Task<Result<long>> Create(CreateOrUpdateCustomerDto dto)
    {
        var entity = _mapper.Map<Customer>(dto);
        var id = await _unitOfWork.CustomerRepository.Create(entity);
        await _unitOfWork.CommitAsync();
        return Result<long>.Success(id);
    }

    public async Task<Result<bool>> Update(CreateOrUpdateCustomerDto dto)
    {
        var entity = _mapper.Map<Customer>(dto);
        bool isSuccess = await _unitOfWork.CustomerRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Updated!");
        else
            return Result<bool>.Failure("Not updated!");
    }

    public async Task<Result<bool>> Delete(long id)
    {
        bool isSuccess = await _unitOfWork.CustomerRepository.DeleteById(id);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Deleted!");
        else
            return Result<bool>.Failure("Not deleted!");
    }
}