using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Infrastructure.Identity;
using System.Reflection;

namespace Sm.Crm.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IApplicationDbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Notification> Notificiations { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<UserEmail> UserEmails { get; set; }
    public DbSet<UserPhone> UserPhones { get; set; }

    // Bu tablo Cookie Authentication dersini örneklendirmek için kullanılmıştır.
    // Kullanıcı girişi için projede ASP.NET Identity yapısı kullanılmaktadır.
    public DbSet<User> Users { get; set; }

    #region LST Schema

    public DbSet<Department> Departments { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<OfferStatus> OfferStatuses { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<RequestStatus> RequestStatuses { get; set; }
    public DbSet<StatusType> StatusTypes { get; set; }
    public DbSet<TaskStatusItem> TaskStatuses { get; set; }
    public DbSet<Territory> Territories { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<UserStatus> UserStatuses { get; set; }

    #endregion

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}