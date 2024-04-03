using Microsoft.EntityFrameworkCore;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Document> Documents { get; }
    DbSet<Employee> Employees { get; }
    DbSet<Notification> Notificiations { get; }
    DbSet<Offer> Offers { get; }
    DbSet<Request> Requests { get; }
    DbSet<Sale> Sales { get; }
    DbSet<TaskItem> Tasks { get; }
    DbSet<User> Users { get; }
    DbSet<UserAddress> UserAddresses { get; }
    DbSet<UserEmail> UserEmails { get; }
    DbSet<UserPhone> UserPhones { get; }

    #region LST Schema

    public DbSet<Department> Departments { get; }
    public DbSet<DocumentType> DocumentTypes { get; }
    public DbSet<OfferStatus> OfferStatuses { get; }
    public DbSet<Region> Regions { get; }
    public DbSet<RequestStatus> RequestStatuses { get; }
    public DbSet<StatusType> StatusTypes { get; }
    public DbSet<TaskStatusItem> TaskStatuses { get; }
    public DbSet<Territory> Territories { get; }
    public DbSet<Title> Titles { get; }
    public DbSet<UserStatus> UserStatuses { get; }

    #endregion

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}