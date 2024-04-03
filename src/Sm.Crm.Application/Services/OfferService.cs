using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Services;

public class OfferService : IOfferService
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IOfferRepository _offerRepository;

    public OfferService(IApplicationDbContext db, IOfferRepository offerRepository, IMapper mapper)
    {
        _context = db;
        _offerRepository = offerRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<OfferDto>>> GetAll()
    {
        var data = await _offerRepository.GetAll().ToListAsync();
        return Result<List<OfferDto>>.Success(_mapper.Map<List<OfferDto>>(data).ToList());
    }

    public async Task<PaginatedResult<OfferDto>> GetPaginated(PaginationRequest req)
    {
        var data = _context.Offers.Include(a => a.EmployeeUserFk).Include(a => a.RequestFk).OrderByDescending(e => e.Id)
             .ProjectTo<OfferDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<OfferDto>.Create(data.AsNoTracking(), req.PageNumber, req.PageSize);
    }

    public async Task<Result<OfferDto?>> GetById(int id)
    {
        var data = await _offerRepository.GetById(id);
        return Result<OfferDto?>.Success(_mapper.Map<OfferDto>(data));
    }

    public async Task<Result<CreateOrEditOfferDto?>> GetFormById(int id)
    {
        var data = await _offerRepository.GetById(id);
        return Result<CreateOrEditOfferDto?>.Success(_mapper.Map<CreateOrEditOfferDto>(data));
    }

    public async Task<Result<int>> Create(CreateOrEditOfferDto offer)
    {
        var entity = _mapper.Map<Offer>(offer);
        var id = await _offerRepository.Create(entity);
        return Result<int>.Success(id);
    }

    public async Task<Result<bool>> Update(CreateOrEditOfferDto offer)
    {
        var entity = _mapper.Map<Offer>(offer);
        bool isSuccess = await _offerRepository.Update(entity);

        if (isSuccess)
            return Result<bool>.Success("Updated!");
        else
            return Result<bool>.Failure("Not updated!");
    }

    public async Task<Result<bool>> Delete(int id)
    {
        bool isSuccess = await _offerRepository.DeleteById(id);

        if (isSuccess)
            return Result<bool>.Success("Deleted!");
        else
            return Result<bool>.Failure("Not deleted!");
    }
}