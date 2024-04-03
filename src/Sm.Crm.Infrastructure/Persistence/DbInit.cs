using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Infrastructure.Persistence.Common;
using Sm.Crm.Infrastructure.Persistence.Seeders;
using System.Reflection;

namespace Sm.Crm.Infrastructure.Persistence;

public static class DbInitExtensions
{
    public static async Task InitializeDb(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        //context.Database.EnsureDeleted();
        //context.Database.EnsureCreated();

        // Migration yapısını kullandığımız durumda proje çalıştığında Migration işlemini yapar.
        //context.Database.Migrate();

        // Users tablosu diğer seeder'larda kullanıldığı için bunu öncelikli yapmasını istedik.
        await new UserSeeder().Seed(context);
        //await new UserAddressSeeder().Seed(context);

        // IdentitySeeder DI yapısıyla çalıştığı için bu şekilde kullandık.
        var identitySeeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();
        await identitySeeder.Seed(context);

        await new RequestStatusSeeder().Seed(context);
        await new TerritorySeeder().Seed(context);
        await new StatusTypeSeeder().Seed(context);
        await new DepartmentSeeder().Seed(context);
        await new EmployeeSeeder().Seed(context);
        await new NotificationSeeder().Seed(context);

        // ISeeder arayüzünden türeyen tüm Seeder'ları çalıştırmayı sağlar.
        await ApplyAllSeederFromAssembly(context);
    }

    private static async Task ApplyAllSeederFromAssembly(ApplicationDbContext context)
    {
        var seederType = typeof(ISeeder);
        var seeders = Assembly.GetExecutingAssembly().GetTypes()
            .Where(s => seederType.IsAssignableFrom(s) && s != seederType)
            .ToList();
        foreach (var type in seeders)
        {
            try
            {
                var seeder = Activator.CreateInstance(type) as ISeeder;
                await seeder?.Seed(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}