using System.Threading.Tasks;

namespace BattleshipsServer.Interfaces
{
    public interface IFileDataStore
    {
        Task<string> Load(string fileName);

        Task Save(string fileName, string content);
    }
}
