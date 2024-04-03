using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Domain.Common;

public interface IUnitOfWork : IDisposable
{
    ICustomerRepository CustomerRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    IUserRepository UserRepository { get; }
    IRequestRepository RequestRepository { get; }
    ISaleRepository SaleRepository { get; }
    IDepartmentRepository DepartmentRepository { get; }
    IUserAddressRepository UserAddressRepository { get; }
    IUserEmailRepository UserEmailRepository { get; }
    ITaskRepository TaskRepository { get; }
    IStatusTypeRepository StatusTypeRepository { get; }
    ITitleRepository TitleRepository { get; }
    INotificationRepository NotificationRepository { get; }

    int Commit();

    Task<int> CommitAsync();
}