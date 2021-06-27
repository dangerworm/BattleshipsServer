using Microsoft.Extensions.DependencyInjection;
using System;

namespace BattleshipsServer.Infrastructure
{
    public static partial class ServiceRegistration
    {
        public static IServiceCollection AddDelegates(this IServiceCollection services)
        {
            return services.AddSingleton<UniqueIdProvider>(Guid.NewGuid);
        }
    }
}