using System.Collections.Generic;
using System.Threading.Tasks;

namespace BattleshipsServer.Interfaces
{
    public interface IDataProvider<TData>
    {
        Task AddItem(TData item);
        Task<IEnumerable<TData>> GetItems();
        Task AddOrEditItem(TData newItem);
        Task EditItem(TData item);
        Task RemoveItem(TData item);
    }
}
