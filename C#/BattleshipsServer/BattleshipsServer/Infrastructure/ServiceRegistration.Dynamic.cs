using BattleshipsServer.Interfaces;
using BattleshipsServer.Validators;
using System;
using System.Linq;
using BattleshipsServer.Data;
using BattleshipsServer.Processors;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceRegistration
    {
        public static IServiceCollection AddProcessors(this IServiceCollection services)
        {
            static bool IsProcessorInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IProcessor<>);
            bool IsProcessor(Type t) => t.GetInterfaces().Any(IsProcessorInterface);

            var processorTypes = typeof(Processor).Assembly.GetTypes().Where(IsProcessor);

            foreach (var processorType in processorTypes)
            {
                var serviceType = processorType.GetInterfaces().Single(IsProcessorInterface);
                services.AddSingleton(serviceType, processorType);
            }

            return services.AddSingleton<IProcessor, Processor>();
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            static bool IsValidatorInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IValidator<>);
            bool IsValidator(Type t) => t.GetInterfaces().Any(IsValidatorInterface);

            var validatorTypes = typeof(Validator).Assembly.GetTypes().Where(IsValidator);

            foreach (var validatorType in validatorTypes)
            {
                var serviceType = validatorType.GetInterfaces().Single(IsValidatorInterface);
                services.AddSingleton(serviceType, validatorType);
            }

            return services.AddSingleton<IValidator, Validator>();
        }

    }
}
