using BattleshipsServer.Models;

namespace BattleshipsServer.Interfaces
{
    public interface IValidator
    {
        ValidatorResult Validate<T>(T value);
    }

    public interface IValidator<T>
    {
        ValidatorResult Validate(T value);
    }
}
