namespace BattleshipsServer.Interfaces
{
    public interface IProcessor
    {
        void Process<T>(T item);
    }

    public interface IProcessor<T>
    {
        void Process(T item);
    }
}
