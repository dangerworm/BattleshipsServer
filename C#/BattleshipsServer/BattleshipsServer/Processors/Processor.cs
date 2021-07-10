using BattleshipsServer.Helpers;
using BattleshipsServer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BattleshipsServer.Enums;

namespace BattleshipsServer.Processors
{
    public class Processor : IProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<Type, object> _processors;

        public Processor(IServiceProvider serviceProvider)
        {
            Verify.NotNull(serviceProvider, nameof(serviceProvider));

            _serviceProvider = serviceProvider;
            _processors = new Dictionary<Type, object>();
        }

        public Task<ProcessorResult> Process<T>(T item, ProcessorOperation operation)
        {
            return GetProcessor<T>().Process(item, operation);
        }

        private IProcessor<T> GetProcessor<T>()
        {
            if (!_processors.ContainsKey(typeof(T)))
            {
                var processor = _serviceProvider.GetService<IProcessor<T>>();
                if (processor == null)
                {
                    throw new InvalidOperationException($"No processor is registered for type {typeof(T)}");
                }

                _processors.Add(typeof(T), processor);
            }

            return _processors[typeof(T)] as IProcessor<T>;
        }
    }
}
