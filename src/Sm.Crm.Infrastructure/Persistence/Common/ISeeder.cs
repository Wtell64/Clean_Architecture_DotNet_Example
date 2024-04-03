using Sm.Crm.Application.Common.Interfaces;

namespace Sm.Crm.Infrastructure.Persistence.Common;

public interface ISeeder
{
    Task Seed(IApplicationDbContext context);
}