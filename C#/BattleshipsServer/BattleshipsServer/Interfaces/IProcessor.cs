using System.Threading.Tasks;
using BattleshipsServer.Enums;
using BattleshipsServer.Processors;

namespace BattleshipsServer.Interfaces
{
    public interface IProcessor
    {
        Task<ProcessorResult> Process<T>(T item, ProcessorOperation operation);
    }

    public interface IProcessor<T>
    {
        Task<ProcessorResult> Process(T item, ProcessorOperation operation);
    }
}
