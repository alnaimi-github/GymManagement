using FluentValidation;
using GymManagement.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
       
         services.AddMediatR(options =>
         {
           options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));

           options.AddOpenBehavior(typeof(ValidationBehavior<,>));
           options.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
         });

         services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

       return services;
    }
}