
using Microsoft.Extensions.DependencyInjection;
using GymManagement.Application.Services;
namespace GymManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
          services.AddScoped<ISubscriptionWriteService,SubscriptionWriteService>();
       return services;
    }
}