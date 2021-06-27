using BattleshipsServer.Data;
using BattleshipsServer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BattleshipsServer.Infrastructure
{
    public static partial class ServiceRegistration
    {
        public static IServiceCollection AddContexts(this IServiceCollection services)
        {
            return services.AddSingleton<IGameContext, GameContext>();
        }

        public static IServiceCollection AddDataStore(this IServiceCollection services)
        {
            return services.AddSingleton<IFileDataStore, LocalDataStore>();
        }
    }
}