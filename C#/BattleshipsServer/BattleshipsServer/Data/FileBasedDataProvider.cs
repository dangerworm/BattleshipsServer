using BattleshipsServer.Helpers;
using BattleshipsServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BattleshipsServer.Data
{
    public abstract class FileBasedDataProvider<TData> : IDataProvider<TData>
    {
        protected abstract string FilePath { get; }

        private readonly IFileDataStore _fileDataStore;

        protected FileBasedDataProvider(IFileDataStore fileDataStore)
        {
            Verify.NotNull(fileDataStore, nameof(fileDataStore));

            _fileDataStore = fileDataStore;
        }

        public async Task AddItem(TData item)
        {
            var serialisedItems = await TryLoad();
            var updatedItems = JsonListHelper.AddItem(serialisedItems, existingItem => IsMatch(existingItem, item), item);
            await _fileDataStore.Save(FilePath, updatedItems);
        }

        public async Task RemoveItem(TData item)
        {
            var serialisedItems = await TryLoad();
            var updatedItems = JsonListHelper.RemoveItem<TData>(serialisedItems, existingItem => IsMatch(existingItem, item));
            await _fileDataStore.Save(FilePath, updatedItems);
        }

        public Task EditItem(TData item)
        {
            throw new NotImplementedException();
        }

        public Task AddOrEditItem(TData newItem)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TData>> GetItems()
        {
            throw new NotImplementedException();
        }

        private async Task<string> TryLoad()
        {
            try
            {
                var itemsJson = await _fileDataStore.Load(FilePath);

                return string.IsNullOrWhiteSpace(itemsJson)
                    ? ""
                    : itemsJson;
            }
            catch (Exception)
            {
                return "";
            }
        }

        protected abstract bool IsMatch(TData existingItem, TData newItem);
    }
}
