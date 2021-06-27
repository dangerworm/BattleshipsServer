using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BattleshipsServer.Interfaces
{
    public interface IDataProvider<TData>
    {
        Task AddItem(TData item);
        Task RemoveItem(TData item);
        Task EditItem(TData item);
        Task AddOrEditItem(TData newItem);
        Task<IEnumerable<TData>> GetItems();
    }
}
