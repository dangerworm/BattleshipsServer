using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BattleshipsServer.Helpers
{
    public static class JsonListHelper
    {
        public static string AddItem<TItem>(string serialisedItems, Func<TItem, bool> isMatch, TItem item)
        {
            var items = DeserialiseItems<TItem>(serialisedItems);

            if (items.Any(isMatch))
            {
                throw new InvalidOperationException($"{GetTypeName<TItem>()} already exists");
            }

            items.Add(item);

            return SerialiseItems(items);
        }

        public static string AddOrEditItem<TItem>(string serialisedItems, Func<TItem, bool> isMatch, Action<TItem, TItem> action, TItem item)
        {
            var items = DeserialiseItems<TItem>(serialisedItems);

            return items.Any(isMatch)
                ? EditItem(serialisedItems, isMatch, action, item)
                : AddItem(serialisedItems, isMatch, item);
        }

        public static string EditItem<TItem>(string serialisedItems, Func<TItem, bool> isMatch, Action<TItem, TItem> action, TItem item)
        {
            var items = DeserialiseItems<TItem>(serialisedItems);

            if (!items.Any(isMatch))
            {
                throw new InvalidOperationException($"{GetTypeName<TItem>()} does not exist");
            }

            action(items.SingleOrDefault(isMatch), item);

            return SerialiseItems(items);
        }

        public static string RemoveItem<TItem>(string serialisedItems, Func<TItem, bool> isMatch)
        {
            var items = DeserialiseItems<TItem>(serialisedItems);

            var item = items.SingleOrDefault(isMatch);
            if (item != null)
            {
                items.Remove(item);
            }

            return SerialiseItems(items);
        }

        public static IList<TItem> DeserialiseItems<TItem>(string serialisedItems)
        {
            return string.IsNullOrWhiteSpace(serialisedItems)
                ? new List<TItem>()
                : JsonSerializer.Deserialize<List<TItem>>(
                    serialisedItems,
                    new JsonSerializerOptions {PropertyNameCaseInsensitive = true}
                );
        }

        private static string SerialiseItems<TItem>(IEnumerable<TItem> deserialisedItems)
        {
            return JsonSerializer.Serialize(
                deserialisedItems,
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true}
            );
        }

        private static string GetTypeName<TItem>()
        {
            return typeof(TItem).ToString().Split(".").Last();
        }
    }
}
