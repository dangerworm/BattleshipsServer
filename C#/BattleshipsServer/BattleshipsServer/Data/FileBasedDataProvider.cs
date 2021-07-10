using BattleshipsServer.Helpers;
using BattleshipsServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BattleshipsServer.Models;

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

            var updatedItems = JsonListHelper.AddItem(serialisedItems, 
                existingItem => IsMatch(existingItem, item), item);

            await _fileDataStore.Save(FilePath, updatedItems);
        }

        public async Task<IEnumerable<TData>> GetItems()
        {
            var serialisedItems = await TryLoad();
            return JsonListHelper.DeserialiseItems<TData>(serialisedItems);
        }

        public async Task AddOrEditItem(TData item)
        {
            var serialisedItems = await TryLoad();

            var updatedItems = JsonListHelper.AddOrEditItem(serialisedItems, 
                existingItem => IsMatch(existingItem, item), EditItem, item);

            await _fileDataStore.Save(FilePath, updatedItems);
        }

        public async Task EditItem(TData item)
        {
            var serialisedItems = await TryLoad();

            var updatedItems = JsonListHelper.EditItem(serialisedItems,
                existingItem => IsMatch(existingItem, item), EditItem, item);

            await _fileDataStore.Save(FilePath, updatedItems);
        }

        public async Task RemoveItem(TData item)
        {
            var serialisedItems = await TryLoad();

            var updatedItems = JsonListHelper.RemoveItem<TData>(serialisedItems, 
                existingItem => IsMatch(existingItem, item));

            await _fileDataStore.Save(FilePath, updatedItems);
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
        protected abstract void EditItem(TData existingItem, TData newItem);
    }
}
