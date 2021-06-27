using BattleshipsServer.Data;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BattleshipsServer.Infrastructure
{
    public static partial class ServiceRegistration
    {
        public static IServiceCollection AddDataProviders(this IServiceCollection services)
        {
            return services
                .AddGameParticipantDataProvider()
                .AddGameSettingsDataProvider();
        }

        public static IServiceCollection AddGameParticipantDataProvider(this IServiceCollection services)
        {
            return services.AddSingleton<IDataProvider<Participant>, ParticipantDataProvider>();
        }

        public static IServiceCollection AddGameSettingsDataProvider(this IServiceCollection services)
        {
            return services.AddSingleton<IDataProvider<GameSettings>, GameSettingsDataProvider>();
        }
    }
}