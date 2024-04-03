using FluentValidation;
using Sm.Crm.Application.Common.Behaviours;
using Sm.Crm.Application.Services;
using Sm.Crm.Application.Services.Interfaces;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        #region BusinessEntityServices
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<ITitleService, TitleService>();
        services.AddTransient<IDocumentService, DocumentService>();
        services.AddTransient<IRequestStatusService, RequestStatusService>();
        services.AddTransient<IDocumentTypeService, DocumentTypeService>();
        services.AddTransient<ISaleService, SaleService>();
        services.AddTransient<IOfferService, OfferService>();
        services.AddTransient<IDepartmentService, DepartmentService>();
        services.AddTransient<ITaskService, TaskService>();
        services.AddTransient<IUserAddressService, UserAddressService>();
        services.AddTransient<IUserEmailService, UserEmailService>();
        services.AddTransient<IEmployeeService, EmployeeService>();
        services.AddTransient<IStatusTypeService, StatusTypeService>();
        services.AddTransient<IRequestService, RequestService>();
        services.AddTransient<INotificationService, NotificationService>();
        #endregion

        var assembly = Assembly.GetExecutingAssembly();

        // AutoMapper
        services.AddAutoMapper(assembly);

        // Fluent Validation
        services.AddValidatorsFromAssembly(assembly);

        // MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);

            //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
        });

        return services;
    }
}