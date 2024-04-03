using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Repositories;
using Sm.Crm.Infrastructure.Persistence.Repositories;

namespace Sm.Crm.Infrastructure.Persistence.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    private ICustomerRepository _customerRepository;
    public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(_context);

    private IUserRepository _userRepository;
    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

    private IEmployeeRepository _employeeRepository;
    public IEmployeeRepository EmployeeRepository => _employeeRepository ??= new EmployeeRepository(_context);

    private ISaleRepository _saleRepository;
    public ISaleRepository SaleRepository => _saleRepository ??= new SaleRepository(_context);

    private IDepartmentRepository _departmentRepository;
    public IDepartmentRepository DepartmentRepository => _departmentRepository ??= new DepartmentRepository(_context);

    private ITaskRepository _taskRepository;
    public ITaskRepository TaskRepository => _taskRepository ??= new TaskRepository(_context);

    private IUserAddressRepository _userAddressRepository;
    public IUserAddressRepository UserAddressRepository => _userAddressRepository ??= new UserAddressRepository(_context);

    private IUserEmailRepository _userEmailRepository;
    public IUserEmailRepository UserEmailRepository => _userEmailRepository ??= new UserEmailRepository(_context);

    private IStatusTypeRepository _statusTypeRepository;
    public IStatusTypeRepository StatusTypeRepository => _statusTypeRepository ??= new StatusTypeRepository(_context);

    private ITitleRepository _titleRepository;
    public ITitleRepository TitleRepository => _titleRepository ??= new TitleRepository(_context);

    private IRequestRepository _requestRepository;
    public IRequestRepository RequestRepository => _requestRepository ??= new RequestRepository(_context);


    private INotificationRepository _notificationRepository;
    public INotificationRepository NotificationRepository => _notificationRepository ??= new NotificationRepository(_context);

    public int Commit()
    {
        return _context.SaveChanges();
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}