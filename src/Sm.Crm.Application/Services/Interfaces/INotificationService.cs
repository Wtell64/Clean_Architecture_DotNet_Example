using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Application.Services.Interfaces;
public interface INotificationService
{
    Task<Result<List<NotificationDto>>> GetAll();

    Task<PaginatedResult<NotificationDto>> GetPaginated(PaginationRequest req);

    Task<Result<NotificationDto?>> GetById(int id);

    Task<Result<int>> Create(CreateOrEditNotificationDto userAddress);

    Task<Result<bool>> Update(CreateOrEditNotificationDto userAddress);

    Task<Result<bool>> Delete(int id);
}
