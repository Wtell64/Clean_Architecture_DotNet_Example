using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models.Email;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Repositories;
using Sm.Crm.Infrastructure.Authentication;
using Sm.Crm.Infrastructure.Caching;
using Sm.Crm.Infrastructure.Email;
using Sm.Crm.Infrastructure.Identity;
using Sm.Crm.Infrastructure.Logging;
using Sm.Crm.Infrastructure.Persistence;
using Sm.Crm.Infrastructure.Persistence.Common;
using Sm.Crm.Infrastructure.Persistence.Interceptors;
using Sm.Crm.Infrastructure.Persistence.Repositories;
using Sm.Crm.Infrastructure.Persistence.Seeders;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton(TimeProvider.System);

        #region Cookie Authentication, ASP.NET Identity

        // Cookie Authentication
        //services.AddScoped<IAccountService, CookieAccountService>();

        services.AddDatabaseDeveloperPageExceptionFilter();
        services
            .AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = false;
                options.Password.RequiredLength = 10;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "SM-CRM";
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
            options.LoginPath = "/App/Account/Login";
            options.AccessDeniedPath = "/App/Account/AccessDenied";
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        });

        services.AddScoped<IAccountService, IdentityAccountService>();
        services.AddScoped<IdentitySeeder>();
        services.AddAuthorization();

        #endregion

        // Generic Repository, Unit of Work Pattern
        services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        #region Repository Dependencies
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOfferRepository, OfferRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IUserAddressRepository, UserAddressRepository>();
        services.AddScoped<IUserEmailRepository, UserEmailRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
        services.AddScoped<IRequestStatusRepository, RequestStatusRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IStatusTypeRepository, StatusTypeRepository>();
        services.AddScoped<ITitleRepository, TitleRepository>();
        services.AddScoped<IRequestRepository, RequestRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

        #endregion

        // Email Service
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));
        services.AddScoped<IEmailService, EmailService>();

        // Caching
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["Redis"];
            options.InstanceName = "RedisOrnek";
        });
        services.AddScoped<IAppCache, RedisCache>();
        
        // Logging
        services.AddSingleton<IAppLogger, LogManager>();

        return services;
    }
}