using BattleshipsServer.Helpers;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace BattleshipsServer.Validators
{
    public class Validator : IValidator
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly Dictionary<Type, object> _validators;

        public Validator(IServiceProvider serviceProvider)
        {
            Verify.NotNull(serviceProvider, nameof(serviceProvider));

            _serviceProvider = serviceProvider;
            _validators = new Dictionary<Type, object>();
        }

        public ValidatorResult Validate<T>(T value)
        {
            return GetValidator<T>().Validate(value);
        }

        private IValidator<T> GetValidator<T>()
        {
            if (!_validators.ContainsKey(typeof(T)))
            {
                var validator = _serviceProvider.GetService<IValidator<T>>();
                if (validator == null)
                {
                    throw new InvalidOperationException($"No validator is registered for type {typeof(T)}");
                }

                _validators.Add(typeof(T), validator);
            }

            return _validators[typeof(T)] as IValidator<T>;
        }
    }
}
