using BattleshipsServer.Data;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using BattleshipsServer.Contexts;

namespace BattleshipsServer.Infrastructure
{
    public static partial class ServiceRegistration
    {
        public static IServiceCollection AddContexts(this IServiceCollection services)
        {
            return services
                .AddSingleton<ITournamentContext, TournamentContext>()
                .AddSingleton<IGamesContext, GamesContext>();
        }

        public static IServiceCollection AddDataStore(this IServiceCollection services)
        {
            services.Configure<FileStoreOptions>(options =>
                options.Path = Path.GetFullPath(Path.Combine(typeof(Program).Assembly.Location, "..", "..", "..", "..")));

            return services.AddTransient<IFileDataStore, LocalDataStore>();
        }
    }
} 